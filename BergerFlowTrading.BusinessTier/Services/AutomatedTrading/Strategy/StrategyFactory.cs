using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy.ArbitrageStrategies;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.TradingUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy
{
    public class StrategyFactory
    {
        private readonly ExchangeFactory exchangeFactory;
        private readonly StrategyLogService logService;

        public StrategyFactory(ExchangeFactory exchangeFactory, StrategyLogService logService)
        {
            this.exchangeFactory = exchangeFactory;
            this.logService = logService;
        }

        public async Task<IStrategy> CreateStrategy(IStrategySettingDTO s, Dictionary<string, SemaphoreSlim> currencySemaphores, SemaphoreSlim concurrentSemaphore)
        {
            IStrategy strat = null;

            if (s is LimitArbitrageStrategy4SettingsDTO)
            {


                var setting = (LimitArbitrageStrategy4SettingsDTO)s;

                var ex1 = await this.exchangeFactory.GetExchange(setting.Exchange_1);
                var ex2 = await this.exchangeFactory.GetExchange(setting.Exchange_2);

                var baseSemapore = this.GetCurrencySemaphore(Currency.GetBaseFromSymbol(setting.Symbol), ref currencySemaphores);
                var quoteSemapore = this.GetCurrencySemaphore(Currency.GetQuoteFromSymbol(setting.Symbol), ref currencySemaphores);

                strat = new LimitArbitrage4Strategy(setting, ex1, ex2, this.logService, baseSemapore, quoteSemapore, concurrentSemaphore);
            }

            return strat;
        }

        private SemaphoreSlim GetCurrencySemaphore(string currency, ref Dictionary<string, SemaphoreSlim> currencySemaphore)
        {
            var sem = currencySemaphore[currency];

            if (sem == null)
            {
                sem = new SemaphoreSlim(1, 1);
                currencySemaphore[currency] = sem;
            }

            return sem;
        }

        public void DisposeOfExchanges(IEnumerable<IStrategy> stopped, IEnumerable<IStrategy> running)
        {
            List<ISpotExchangeFacade> stillNeeded = new List<ISpotExchangeFacade>();
            List<ISpotExchangeFacade> stoppedRelatedExchanges = new List<ISpotExchangeFacade>();

            foreach(IStrategy s in running)
            {
                if (s.strategyInfo is LimitArbitrageStrategy4SettingsDTO)
                {
                    var setting = (LimitArbitrageStrategy4SettingsDTO)s;

                    var ex1 = this.exchangeFactory.exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == setting.Exchange_1.Name);
                    var ex2 = this.exchangeFactory.exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == setting.Exchange_2.Name);

                    stillNeeded.Add(ex1);
                    stillNeeded.Add(ex2);
                }
            }

            foreach (IStrategy s in stopped)
            {
                if (s.strategyInfo is LimitArbitrageStrategy4SettingsDTO)
                {
                    var setting = (LimitArbitrageStrategy4SettingsDTO)s;

                    var ex1 = this.exchangeFactory.exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == setting.Exchange_1.Name);
                    var ex2 = this.exchangeFactory.exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == setting.Exchange_2.Name);

                    stoppedRelatedExchanges.Add(ex1);
                    stoppedRelatedExchanges.Add(ex2);
                }
            }

            var toDispose = stoppedRelatedExchanges.Where(x => !stillNeeded.Contains(x));

            foreach(ISpotExchangeFacade fac in toDispose)
            {
                exchangeFactory.DisposeOf(fac);
            }
        }
    }
}
