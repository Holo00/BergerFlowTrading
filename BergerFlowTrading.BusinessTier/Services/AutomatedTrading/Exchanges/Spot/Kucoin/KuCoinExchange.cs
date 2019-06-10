using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Kucoin
{
    public class KuCoinExchange : SpotExchange_WSS_API
    {
        public KuCoinExchange(ILoggingService logger, ExchangeDTO exchangeSettings, UserExchangeSecretDTO secrets)
            : base(logger, true, true, exchangeSettings, secrets)
        {
            this.api = new KuCoinApi(exchangeSettings, logger, secrets);
            this.wss = new KuCoinWSS();
            this.reader = new KuCoinReader();
        }
    }
}
