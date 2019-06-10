using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Binance
{
    public class BinanceExchange : SpotExchange_WSS_API
    {
        public BinanceExchange(ILoggingService logger, ExchangeDTO exchangeSettings, UserExchangeSecretDTO secrets)
            : base(logger, true, true, exchangeSettings, secrets)
        {
            this.api = new BinanceApi(exchangeSettings, logger, secrets);
            this.wss = new BinanceWSS();
            this.reader = new BinanceReader();

        }
    }
}
