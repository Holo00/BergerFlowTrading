using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.HitBTC
{
    public class HitBTCExchange : SpotExchange_WSS_API
    {
        public HitBTCExchange(ExchangeDTO exchangeSettings, UserExchangeSecretDTO secrets, ExchangeLogService logService)
            : base(true, true, exchangeSettings, secrets, logService)
        {
            this.api = new HitBTCApi(exchangeSettings, logService, secrets);
            this.wss = new HitBTCWSS();
            this.reader = new HitBTCReader();
        }
    }
}
