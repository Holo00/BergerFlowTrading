using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Binance
{
    public class BinanceExchange : SpotExchange_WSS_API
    {
        public BinanceExchange(ExchangeDTO exchangeSettings, UserExchangeSecretDTO secrets, ExchangeLogService logService)
            : base(true, true, exchangeSettings, secrets, logService)
        {
            this.api = new BinanceApi(exchangeSettings, logService, secrets);
            this.wss = new BinanceWSS();
            this.reader = new BinanceReader();

        }
    }
}
