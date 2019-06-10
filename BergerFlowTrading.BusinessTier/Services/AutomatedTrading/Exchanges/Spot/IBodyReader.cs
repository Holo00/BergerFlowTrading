using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public interface IBodyReader
    {
        Balance ReadBalance_API(string body);
        List<Balance> ReadBalances_API(string body);
        Balance ReadBalance_WSS(string body);
        List<Balance> ReadBalances_WSS(string body);



        Candle ReadCandle_API(string body, CandlePeriod p);
        List<Candle> ReadCandles_API(string body, CandlePeriod p);
        Candle ReadCandle_WSS(string body, CandlePeriod p);
        List<Candle> ReadCandles_WSS(string body, CandlePeriod p);



        Order ReadOrder_API(string body, string symbol = null);
        List<Order> ReadOrders_API(string body, string symbol = null);
        Order ReadOrder_WSS(string body, string symbol = null);
        List<Order> ReadOrders_WSS(string body, string symbol = null);



        OrderbookRecord ReadOrderbook_API(string body, string symbol = null);
        List<OrderbookRecord> ReadOrderbooks_API(string body, string symbol = null);
        OrderbookRecord ReadOrderbook_WSS(string body, string symbol = null);
        List<OrderbookRecord> ReadOrderbooks_WSS(string body, string symbol = null);



        Symbol ReadSymbol_API(string body);
        List<Symbol> ReadSymbols_API(string body);
        Symbol ReadSymbol_WSS(string body);
        List<Symbol> ReadSymbols_WSS(string body);


        Trade ReadTrade_API(string body, string symbol = null);
        List<Trade> ReadTrades_API(string body, string symbol = null);
        Trade ReadTrade_WSS(string body, string symbol = null);
        List<Trade> ReadTrades_WSS(string body, string symbol = null);
    }
}
