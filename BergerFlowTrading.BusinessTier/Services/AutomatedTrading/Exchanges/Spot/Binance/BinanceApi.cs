using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
using BergerFlowTrading.Shared.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Binance
{
    public class BinanceApi : SpotExchangeApiBase
    {
        public BinanceApi(ExchangeDTO exchangeSettings, ILoggingService logger, UserExchangeSecretDTO secrets) : base(exchangeSettings, logger, secrets)
        {

        }

        protected override Task<HttpResponseResult> _GetBalance(string currency, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpResponseResult> _GetBalances(bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpResponseResult> _GetCandles(string symbol, CandlePeriod p, int limit, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpResponseResult> _GetOrderbooks(string symbol, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpResponseResult> _GetOrders(string symbol, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpResponseResult> _GetOwnTrades(string symbol, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpResponseResult> _GetSymbol(string symbol, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpResponseResult> _GetSymbols(bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpResponseResult> _GetTrades(string symbol, bool force = false)
        {
            throw new NotImplementedException();
        }
    }
}
