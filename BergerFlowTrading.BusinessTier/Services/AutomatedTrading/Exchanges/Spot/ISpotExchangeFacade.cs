using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public interface ISpotExchangeFacade: IDisposable
    {
        ExchangeName ExchangeName { get; }

        List<ExchangeModel> exchangeModel { get; set; }

        Task LoadSymbols();
        Task LoadBalances();


        Task RefreshBalance(string symbol, bool force = false);
        Task RefreshCandles(string symbol, CandlePeriod p, int limit, bool force = false);
        Task RefreshOrderbooks(string symbol, bool force = false);
        Task RefreshOrders(string symbol, bool force = false);
        Task RefreshOwnTrades(string symbol, bool force = false);
        Task RefreshSymbol(string symbol, bool force = false);
        Task RefreshTrades(string symbol, bool force = false);


        Task StartObserveSymbol(string symbol);
        Task StopObserveSymbol(string symbol);


        Task StartObserveOrders(string symbol);
        Task StopObserveOrders(string symbol);


        Task StartObserveOrderbooks(string symbol);
        Task StopObserveOrderbooks(string symbol);


        Task StartObserveBalance(string symbol);
        Task StopObserveBalances(string symbol);


        Task StartObserveTrades(string symbol);
        Task StopObserveTrades(string symbol);


        Task StartObserveOwnTrades(string symbol);
        Task StopObserveOwnTrades(string symbol);


        Task StartObserveCandle(string symbol, CandlePeriod p);
        Task StopObserveCandle(string symbol, CandlePeriod p);
    }
}
