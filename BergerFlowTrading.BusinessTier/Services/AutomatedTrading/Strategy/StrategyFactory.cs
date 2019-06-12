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
        private readonly ILoggingService logService;

        public StrategyFactory(ExchangeFactory exchangeFactory, ILoggingService logService)
        {
            this.exchangeFactory = exchangeFactory;
            this.logService = logService;
        }

        public IStrategy CreateStrategy(IStrategySettingDTO s, ref Dictionary<string, SemaphoreSlim> currencySemaphores, ref SemaphoreSlim concurrentSemaphore)
        {
            IStrategy strat = null;

            if (s is LimitArbitrageStrategy4SettingsDTO)
            {
                var setting = (LimitArbitrageStrategy4SettingsDTO)s;

                var ex1 = this.exchangeFactory.exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == setting.Exchange_1.Name);
                var ex2 = this.exchangeFactory.exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == setting.Exchange_2.Name);

                var baseSemapore = this.GetCurrencySemaphore(Currency.GetBaseFromSymbol(setting.Symbol), ref currencySemaphores);
                var quoteSemapore = this.GetCurrencySemaphore(Currency.GetQuoteFromSymbol(setting.Symbol), ref currencySemaphores);

                strat = new LimitArbitrage4Strategy(setting, ex1, ex2, logService, ref baseSemapore, ref quoteSemapore, ref concurrentSemaphore);
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
    }
}
