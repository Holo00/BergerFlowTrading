using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.HitBTC
{
    public class HitBTCExchange : SpotExchange_WSS_API
    {
        public HitBTCExchange(ILoggingService logger, ExchangeDTO exchangeSettings, UserExchangeSecretDTO secrets)
            : base(logger, true, true, exchangeSettings, secrets)
        {
            this.api = new HitBTCApi(exchangeSettings, logger, secrets);
            this.wss = new HitBTCWSS();
            this.reader = new HitBTCReader();
        }
    }
}
