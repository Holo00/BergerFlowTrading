using BergerFlowTrading.Shared.DTO.Trading;
using BergerFlowTrading.Shared.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public interface ISpotExchangeApi
    {
        Task<HttpResponseResult> GetBalance(string currency, bool force = false);
        Task<HttpResponseResult> GetCandles(string symbol, CandlePeriod p, int limit, bool force = false);
        Task<HttpResponseResult> GetOrderbooks(string symbol, bool force = false);
        Task<HttpResponseResult> GetOrders(string symbol, bool force = false);
        Task<HttpResponseResult> GetOwnTrades(string symbol, bool force = false);
        Task<HttpResponseResult> GetSymbol(string symbol, bool force = false);
        Task<HttpResponseResult> GetTrades(string symbol, bool force = false);
        Task<HttpResponseResult> GetSymbols(bool force = false);
        Task<HttpResponseResult> GetBalances(bool force = false);
    }
}
