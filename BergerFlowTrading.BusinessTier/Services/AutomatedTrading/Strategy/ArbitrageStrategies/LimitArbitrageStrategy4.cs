using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Data.Logs;
using BergerFlowTrading.Shared.DTO.Trading;
using BergerFlowTrading.Shared.DTO.Trading.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy.ArbitrageStrategies
{
    public class LimitArbitrage4Strategy : IStrategy
    {
        private CancellationTokenSource token { get; set; }

        public bool IsRunning { get; private set; }

        public string Name { get; private set; }

        public StrategyType Type { get; private set; }

        private LimitArbitrageStrategy4SettingsDTO settings { get; set; }

        private ISpotExchangeFacade exchangefacade1 { get; set; }
        private ISpotExchangeFacade exchangefacade2 { get; set; }

        private Limit4DealInProgress dealInprogress { get; set; }

        private string symbol { get; set; }

        private StrategyLogService strategyLogService { get; set; }

        private SemaphoreSlim baseSemaphore { get; set; }
        private SemaphoreSlim quoteSemaphore { get; set; }
        private SemaphoreSlim concurrentSemaphores { get; set; }

        public IStrategySettingDTO strategyInfo { get {return this.settings;} }

        public LimitArbitrage4Strategy(LimitArbitrageStrategy4SettingsDTO settings,
                                        ISpotExchangeFacade exchangefacade1,
                                        ISpotExchangeFacade exchangefacade2,
                                        StrategyLogService strategyLogService,
                                        SemaphoreSlim baseSemaphore,
                                        SemaphoreSlim quoteSemaphore,
                                        SemaphoreSlim concurrentSemaphores)
        {
            this.IsRunning = false;
            this.Type = StrategyType.LimitArbitrage4;

            this.settings = settings;
            this.exchangefacade1 = exchangefacade1;
            this.exchangefacade2 = exchangefacade2;

            this.symbol = settings.Symbol;

            this.strategyLogService = strategyLogService;

            this.baseSemaphore = baseSemaphore;
            this.quoteSemaphore = quoteSemaphore;
            this.concurrentSemaphores = concurrentSemaphores;
            this.Name = this.settings.StrategyName;
        }

        public async Task Start(CancellationTokenSource token)
        {
            try
            {
                if (!IsRunning)
                {
                    this.strategyLogService.Log(this.settings.ID, this.settings.User_ID, $"Starting strategy...", eventType.Info);
                    List<Task> tasks = new List<Task>();
                    tasks.Add(this.exchangefacade1.StartObserveBalance(symbol));
                    tasks.Add(this.exchangefacade2.StartObserveBalance(symbol));
                    tasks.Add(this.exchangefacade1.StartObserveOrderbooks(symbol));
                    tasks.Add(this.exchangefacade2.StartObserveOrderbooks(symbol));
                    tasks.Add(this.exchangefacade1.StartObserveOwnTrades(symbol));
                    tasks.Add(this.exchangefacade2.StartObserveOwnTrades(symbol));

                    await Task.WhenAll(tasks);

                    this.dealInprogress = new Limit4DealInProgress(){
                        Limit4CurrentMode = Limit4CurrentMode.None
                    };

                    this.token = token;

                    this.ExecuteStrategy(token.Token);
                    this.strategyLogService.Log(this.settings.ID, this.settings.User_ID, $"Strategy started sucessfully...", eventType.Info);
                }

                this.IsRunning = true;
            }
            catch (Exception ex)
            {
                this.strategyLogService.LogException(this.settings.ID, this.settings.User_ID, ex);
            }
        }

        public async Task Stop()
        {
            try
            {
                if (IsRunning)
                {
                    this.strategyLogService.Log(this.settings.ID, this.settings.User_ID, $"Stopping strategy...", eventType.Info);

                    this.exchangefacade1.StopObserveBalances(symbol);
                    this.exchangefacade2.StopObserveBalances(symbol);

                    this.exchangefacade1.StopObserveOrderbooks(symbol);
                    this.exchangefacade2.StopObserveOrderbooks(symbol);

                    this.exchangefacade1.StopObserveOwnTrades(symbol);
                    this.exchangefacade2.StopObserveOwnTrades(symbol);

                    this.token.Cancel();
                    this.IsRunning = false;
                    this.strategyLogService.Log(this.settings.ID, this.settings.User_ID, $"Strategy stopped successfully...", eventType.Info);
                }
            }
            catch (Exception ex)
            {
                this.strategyLogService.LogException(this.settings.ID, this.settings.User_ID, ex);
            }
        }

        private async Task ExecuteStrategy(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    this.strategyLogService.Log(this.settings.ID, this.settings.User_ID, $"Waiting to execute strategy...", eventType.Info);
                    await quoteSemaphore.WaitAsync();
                    await baseSemaphore.WaitAsync();
                    await concurrentSemaphores.WaitAsync();

                    this.strategyLogService.Log(this.settings.ID, this.settings.User_ID, $"Executing Strategy...", eventType.Info);
                    await this.ExecuteAlgorithm();
                }
                catch (Exception ex)
                {
                    this.strategyLogService.LogException(this.settings.ID, this.settings.User_ID, ex);
                }
                finally
                {
                    this.strategyLogService.Log(this.settings.ID, this.settings.User_ID, $"Finishing executing Strategy...", eventType.Info);
                    quoteSemaphore.Release();
                    baseSemaphore.Release();
                    concurrentSemaphores.Release();
                    await Task.Delay(60000, this.token.Token);
                }
            }
        }

        private async Task ExecuteAlgorithm()
        {
            ExchangeModel ex1Model = exchangefacade1.exchangeModel.FirstOrDefault(x => x.Symbol.Value.symbol == settings.Symbol);
            ExchangeModel ex2Model = exchangefacade2.exchangeModel.FirstOrDefault(x => x.Symbol.Value.symbol == settings.Symbol);

            if (this.settings.ManagementBalanceON && (this.dealInprogress.Limit4CurrentMode == Limit4CurrentMode.None || this.dealInprogress.Limit4CurrentMode == Limit4CurrentMode.ManageBalance)) //Must Buy or Sell
            {
                this.dealInprogress.Limit4CurrentMode = Limit4CurrentMode.ManageBalance;

                //Cancel all orders
                //Place buy orders
                //Place sells orders
            }
            else if(this.settings.Active) //Regular trading algo
            {
                //Verify Orders are right
            }
        }

        private void ExecuteBalanceManagement(ExchangeModel ex1Model, ExchangeModel ex2Model)
        {
            //Check if a balance should be held
            if ((ex1Model.ATRPerc(CandlePeriod.D1, 10) + ex2Model.ATRPerc(CandlePeriod.D1, 10)) / 2 > settings.MinATRValue
                && ex1Model.MidPrice.Value > settings.Min_Price && ex1Model.MidPrice.Value < settings.Max_Price
                && ex2Model.MidPrice.Value > settings.Min_Price && ex2Model.MidPrice.Value < settings.Max_Price
                )
            {
                if(settings.Value_Currency == ValueCurrency.USD)
                {

                }
                else if(settings.Value_Currency == ValueCurrency.BTC)
                {

                }
                else if (settings.Value_Currency == ValueCurrency.Base)
                {

                }
                else if (settings.Value_Currency == ValueCurrency.Quote)
                {

                }
            }
        }



        public void Dispose()
        {
            this.strategyLogService.Log(this.settings.ID, this.settings.User_ID, $"Disposing of Strategy...",  eventType.Info);
            Task.Run(async () => { await this.Stop(); }).Wait();
            GC.SuppressFinalize(this);
        }
    }
}
