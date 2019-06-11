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

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy
{
    public class StrategyFactory
    {


        public IStrategy CreateStrategy(IStrategySettingDTO s
                                                , Func<string, SemaphoreSlim> GetCurrencySemaphore
                                                , SemaphoreSlim concurrentSemaphore
                                                , ILoggingService log
            )
        {
            IStrategy strat = null;

            if (s is LimitArbitrageStrategy4SettingsDTO)
            {
                LimitArbitrageStrategy4SettingsDTO dto = (LimitArbitrageStrategy4SettingsDTO)s;

                ISpotExchangeFacade facade1 = exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == dto.Exchange_1.Name);
                ISpotExchangeFacade facade2 = exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == dto.Exchange_2.Name);

                SemaphoreSlim baseSem = GetCurrencySemaphore(Currency.GetBaseFromSymbol(dto.Symbol));
                SemaphoreSlim quoteSem = GetCurrencySemaphore(Currency.GetQuoteFromSymbol(dto.Symbol));

                strat = new LimitArbitrage4Strategy(dto, facade1, facade2, log, ref baseSem, ref quoteSem, ref concurrentSemaphore);
            }

            return strat;
        }
    }
}
