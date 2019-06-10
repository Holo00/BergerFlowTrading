using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Binance;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.HitBTC;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Kucoin;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy.ArbitrageStrategies;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
using BergerFlowTrading.Shared.TradingUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading
{
    public class TradingPlatform
    {

        private readonly LimitStrategy4SettingsRepository repoSettings;
        private readonly UserExchangeSecretRepository repoExchangeSecrets;
        private IdentityService identityService { get; set; }
        private ILoggingService logger { get; set; }

        public Dictionary<string, CancellationTokenSource> strategyTokens { get; private set; }

        private SemaphoreSlim concurrentSemaphore;
        private Dictionary<string, SemaphoreSlim> currencySemaphores { get; set; }



        public TradingPlatform(LimitStrategy4SettingsRepository repoSettings
                                , UserExchangeSecretRepository repoExchangeSecrets
                                , IdentityService identityService
                                , ILoggingService logger


            )
        {
            this.repoSettings = repoSettings;
            this.repoExchangeSecrets = repoExchangeSecrets;
            this.identityService = identityService;
            this.strategyTokens = new Dictionary<string, CancellationTokenSource>();

            this.concurrentSemaphore = new SemaphoreSlim(100, 100);
            this.currencySemaphores = new Dictionary<string, SemaphoreSlim>();
        }

        public async Task StartStrategies(CancellationToken stoppingToken, string username, string password)
        {
            List<LimitArbitrageStrategy4SettingsDTO> limit4ToExecute = await repoSettings.GetActivesOrBalanceManage();

            List<ExchangeDTO> exchanges = limit4ToExecute.Select(x => x.Exchange_1)
                                                        .Union(limit4ToExecute.Select(x => x.Exchange_2))
                                                        .Distinct().ToList();

            List<ISpotExchangeFacade> exchangeFacades = new List<ISpotExchangeFacade>();

            foreach (ExchangeDTO exx in exchanges)
            {
                ISpotExchangeFacade fac = null;

                if (exx.Name == ExchangeName.Binance.ToString())
                {
                    UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID.Value);
                    fac = new BinanceExchange(this.logger, exx, secrets);
                }
                else if (exx.Name == ExchangeName.HitBTC.ToString())
                {
                    UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID.Value);
                    fac = new HitBTCExchange(this.logger, exx, secrets);
                }
                else if (exx.Name == ExchangeName.KuCoin.ToString())
                {
                    UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID.Value);
                    fac = new KuCoinExchange(this.logger, exx, secrets);
                }

                if (fac != null)
                {
                    exchangeFacades.Add(fac);
                }
            }


            List<IStrategy> strategies = new List<IStrategy>();


            //Set limit 4 strategies
            foreach (LimitArbitrageStrategy4SettingsDTO set in limit4ToExecute)
            {
                ISpotExchangeFacade facade1 = exchangeFacades.FirstOrDefault(x => x.ExchangeName.ToString() == set.Exchange_1.Name);
                ISpotExchangeFacade facade2 = exchangeFacades.FirstOrDefault(x => x.ExchangeName.ToString() == set.Exchange_2.Name);

                SemaphoreSlim baseSem = this.GetCurrencySemaphore(Currency.GetBaseFromSymbol(set.Symbol));
                SemaphoreSlim quoteSem = this.GetCurrencySemaphore(Currency.GetQuoteFromSymbol(set.Symbol));



                IStrategy strat = new LimitArbitrage4Strategy(set, facade1, facade2, this.logger, ref baseSem, ref quoteSem, ref concurrentSemaphore);
                strategies.Add(strat);
            }



            //Start Strategies
            foreach (IStrategy strat in strategies)
            {
                CancellationTokenSource token = new CancellationTokenSource();
                this.strategyTokens.Add(strat.Name, token);
                await strat.Start(token);
            }
        }

        public SemaphoreSlim GetCurrencySemaphore(string currency)
        {
            var sem = currencySemaphores[currency];

            if (sem == null)
            {
                sem = new SemaphoreSlim(1, 1);
                currencySemaphores[currency] = sem;
            }

            return sem;
        }

        public async Task StopAllStrategies()
        {
            foreach (KeyValuePair<string, CancellationTokenSource> k in this.strategyTokens)
            {
                await this.StopStrategy(k.Key);
            }
        }

        public async Task StopStrategy(string name)
        {
            var token = this.strategyTokens[name];
            token.Cancel();
        }
    }
}
