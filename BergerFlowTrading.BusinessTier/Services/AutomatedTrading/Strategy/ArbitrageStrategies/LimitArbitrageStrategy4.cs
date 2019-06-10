using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
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

        public string Name => throw new NotImplementedException();

        public StrategyType Type { get; private set; }

        private LimitArbitrageStrategy4SettingsDTO settings { get; set; }

        private ISpotExchangeFacade exchangefacade1 { get; set; }
        private ISpotExchangeFacade exchangefacade2 { get; set; }



        private ExchangeModel exchangeModel1 { get; set; }
        private ExchangeModel exchangeModel2 { get; set; }

        private ILoggingService logger { get; set; }

        private string symbol { get; set; }

        private SemaphoreSlim baseSemaphore { get; set; }
        private SemaphoreSlim quoteSemaphore { get; set; }
        private SemaphoreSlim concurrentSemaphores { get; set; }



        public LimitArbitrage4Strategy(LimitArbitrageStrategy4SettingsDTO settings,
                                        ISpotExchangeFacade exchangefacade1,
                                        ISpotExchangeFacade exchangefacade2,
                                        ILoggingService logger,
                                        ref SemaphoreSlim baseSemaphore,
                                        ref SemaphoreSlim quoteSemaphore,
                                        ref SemaphoreSlim concurrentSemaphores)
        {
            this.IsRunning = false;
            this.Type = StrategyType.LimitArbitrage4;

            this.settings = settings;
            this.exchangefacade1 = exchangefacade1;
            this.exchangefacade2 = exchangefacade2;
            this.exchangeModel1 = exchangefacade1.exchangeModel.FirstOrDefault(x => x.Symbol.Value.symbol == settings.Symbol);
            this.exchangeModel2 = exchangefacade2.exchangeModel.FirstOrDefault(x => x.Symbol.Value.symbol == settings.Symbol);

            this.logger = logger;

            this.symbol = settings.Symbol;

            this.baseSemaphore = baseSemaphore;
            this.quoteSemaphore = quoteSemaphore;
            this.concurrentSemaphores = concurrentSemaphores;
        }

        public async Task Start(CancellationTokenSource token)
        {
            try
            {
                if (!IsRunning)
                {
                    List<Task> tasks = new List<Task>();
                    tasks.Add(this.exchangefacade1.StartObserveBalance(symbol));
                    tasks.Add(this.exchangefacade2.StartObserveBalance(symbol));
                    tasks.Add(this.exchangefacade1.StartObserveOrderbooks(symbol));
                    tasks.Add(this.exchangefacade2.StartObserveOrderbooks(symbol));
                    tasks.Add(this.exchangefacade1.StartObserveOwnTrades(symbol));
                    tasks.Add(this.exchangefacade2.StartObserveOwnTrades(symbol));

                    await Task.WhenAll(tasks);

                    this.token = token;

                    this.ExecuteStrategy(token.Token);
                }

                this.IsRunning = true;
            }
            catch (Exception ex)
            {
                this.logger.Log(ex);
            }
        }

        public async Task Stop()
        {
            try
            {
                if (IsRunning)
                {
                    this.exchangefacade1.StopObserveBalances(symbol);
                    this.exchangefacade2.StopObserveBalances(symbol);

                    this.exchangefacade1.StopObserveOrderbooks(symbol);
                    this.exchangefacade2.StopObserveOrderbooks(symbol);

                    this.exchangefacade1.StopObserveOwnTrades(symbol);
                    this.exchangefacade2.StopObserveOwnTrades(symbol);

                    this.token.Cancel();
                    this.IsRunning = false;
                }
            }
            catch (Exception ex)
            {
                this.logger.Log(ex);
            }
        }

        private async Task ExecuteStrategy(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await quoteSemaphore.WaitAsync();
                    await baseSemaphore.WaitAsync();
                    await concurrentSemaphores.WaitAsync();

                    await this.ExecuteAlgorithm();
                }
                catch (Exception ex)
                {
                    logger.Log(ex);
                }
                finally
                {
                    quoteSemaphore.Release();
                    baseSemaphore.Release();
                    concurrentSemaphores.Release();
                    await Task.Delay(60000);
                }
            }
        }

        private async Task ExecuteAlgorithm()
        {
            if (true) //Must Buy or Sell
            {
                //Cancel all orders
                //Place buy orders
                //Place sells orders
            }
            else //Regular algo
            {
                //Verify Orders are right
            }
        }
    }
}
