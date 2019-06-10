using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.HitBTC
{
    public class HitBTCReader : IBodyReader
    {
        public List<Balance> ReadBalances_API(string body)
        {
            throw new NotImplementedException();
        }

        public List<Balance> ReadBalances_WSS(string body)
        {
            throw new NotImplementedException();
        }

        public Balance ReadBalance_API(string body)
        {
            throw new NotImplementedException();
        }

        public Balance ReadBalance_WSS(string body)
        {
            throw new NotImplementedException();
        }

        public List<Candle> ReadCandles_API(string body, CandlePeriod p)
        {
            throw new NotImplementedException();
        }

        public List<Candle> ReadCandles_WSS(string body, CandlePeriod p)
        {
            throw new NotImplementedException();
        }

        public Candle ReadCandle_API(string body, CandlePeriod p)
        {
            throw new NotImplementedException();
        }

        public Candle ReadCandle_WSS(string body, CandlePeriod p)
        {
            throw new NotImplementedException();
        }

        public List<OrderbookRecord> ReadOrderbooks_API(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public List<OrderbookRecord> ReadOrderbooks_WSS(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public OrderbookRecord ReadOrderbook_API(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public OrderbookRecord ReadOrderbook_WSS(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public List<Order> ReadOrders_API(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public List<Order> ReadOrders_WSS(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Order ReadOrder_API(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Order ReadOrder_WSS(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public List<Symbol> ReadSymbols_API(string body)
        {
            throw new NotImplementedException();
        }

        public List<Symbol> ReadSymbols_WSS(string body)
        {
            throw new NotImplementedException();
        }

        public Symbol ReadSymbol_API(string body)
        {
            throw new NotImplementedException();
        }

        public Symbol ReadSymbol_WSS(string body)
        {
            throw new NotImplementedException();
        }

        public List<Trade> ReadTrades_API(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public List<Trade> ReadTrades_WSS(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Trade ReadTrade_API(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Trade ReadTrade_WSS(string body, string symbol = null)
        {
            throw new NotImplementedException();
        }
    }
}
