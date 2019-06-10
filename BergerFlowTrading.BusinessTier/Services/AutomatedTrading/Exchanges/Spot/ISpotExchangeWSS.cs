using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public interface ISpotExchangeWSS
    {
        Task<bool> SubscribeToBalances();
        Task<bool> UnSubscribeToBalances();

        Task<bool> SubscribeToSymbols();
        Task<bool> UnSubscribeToSymbols();

        Task<bool> SubscribeToBalance(string currency);
        Task<bool> UnSubscribeToBalance(string currency);

        Task<bool> SubscribeToSymbol(string symbol);
        Task<bool> UnSubscribeToSymbol(string symbol);

        Task<bool> SubscribeToCandles(string symbol, CandlePeriod p);
        Task<bool> UnSubscribeToCandles(string symbol, CandlePeriod p);

        Task<bool> SubscribeToOrderbook(string symbol);
        Task<bool> UnSubscribeToOrderbook(string symbol);

        Task<bool> SubscribeToOrders(string symbol);
        Task<bool> UnSubscribeToOrders(string symbol);

        Task<bool> SubscribeToOwnTrades(string symbol);
        Task<bool> UnSubscribeToOwnTrades(string symbol);

        Task<bool> SubscribeToTrades(string symbol);
        Task<bool> UnSubscribeToTrades(string symbol);
    }
}
