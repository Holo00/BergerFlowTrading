using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.HitBTC
{
    public class HitBTCWSS : ISpotExchangeWSS
    {
        public Task<bool> SubscribeToBalance(string currency)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToBalances()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToCandles(string symbol, CandlePeriod p)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToOrderbook(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToOrders(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToOwnTrades(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToSymbol(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToSymbols()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToTrades(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToBalance(string currency)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToBalances()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToCandles(string symbol, CandlePeriod p)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToOrderbook(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToOrders(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToOwnTrades(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToSymbol(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToSymbols()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeToTrades(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
