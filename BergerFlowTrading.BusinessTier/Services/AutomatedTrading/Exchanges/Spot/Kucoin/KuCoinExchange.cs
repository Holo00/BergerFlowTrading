using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Kucoin
{
    public class KuCoinExchange : SpotExchange_WSS_API
    {
        public KuCoinExchange(ExchangeDTO exchangeSettings, UserExchangeSecretDTO secrets, ExchangeLogService logService)
            : base(true, true, exchangeSettings, secrets, logService)
        {
            this.api = new KuCoinApi(exchangeSettings, logService, secrets);
            this.wss = new KuCoinWSS();
            this.reader = new KuCoinReader();
        }
    }
}
