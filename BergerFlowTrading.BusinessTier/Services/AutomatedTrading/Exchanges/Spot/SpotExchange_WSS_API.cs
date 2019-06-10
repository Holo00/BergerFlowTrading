using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public abstract class SpotExchange_WSS_API : SpotExchangeBase
    {
        protected ISpotExchangeWSS wss { get; set; }

        public SpotExchange_WSS_API(ILoggingService logger, bool ObserveAllSymbolsMode, bool ObserveAllBalancesMode, ExchangeDTO exchangeSettings, UserExchangeSecretDTO secrets)
            : base(logger, ObserveAllSymbolsMode, ObserveAllBalancesMode, exchangeSettings, secrets)
        {
        }


        protected override async Task ObserveSymbol_All(CancellationTokenSource token)
        {
            await this.wss.SubscribeToSymbols();
        }

        protected override async Task ObserveSymbol_All_Stop(CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToSymbols();
        }


        protected override async Task ObserveSymbol_One(string symbol, CancellationTokenSource token)
        {
            await this.wss.SubscribeToSymbol(symbol);
        }

        protected override async Task ObserveSymbol_One_Stop(string symbol, CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToSymbol(symbol);
        }


        protected override async Task ObserveBalance_All(CancellationTokenSource token)
        {
            await this.wss.SubscribeToBalances();
        }

        protected override async Task ObserveBalance_All_Stop(CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToBalances();
        }



        protected override async Task ObserveBalance_One(string currency, CancellationTokenSource token)
        {
            await this.wss.SubscribeToBalance(currency);
        }

        protected override async Task ObserveBalance_One_Stop(string currency, CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToBalance(currency);
        }



        protected override async Task ObserveOrders(string symbol, CancellationTokenSource token)
        {
            await this.wss.SubscribeToOrders(symbol);
        }

        protected override async Task ObserveOrders_Stop(string symbol, CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToOrders(symbol);
        }



        protected override async Task ObserveOrderbook(string symbol, CancellationTokenSource token)
        {
            await this.wss.SubscribeToOrderbook(symbol);
        }

        protected override async Task ObserveOrderbook_Stop(string symbol, CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToOrderbook(symbol);
        }



        protected override async Task ObserveTrades(string symbol, CancellationTokenSource token)
        {
            await this.wss.SubscribeToTrades(symbol);
        }

        protected override async Task ObserveTrades_Stop(string symbol, CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToTrades(symbol);
        }

        protected override async Task ObserveOwnTrades(string symbol, CancellationTokenSource token)
        {
            await this.wss.SubscribeToOwnTrades(symbol);
        }

        protected override async Task ObserveOwnTrades_Stop(string symbol, CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToOwnTrades(symbol);
        }



        protected override async Task ObserveCandles(string symbol, CandlePeriod p, CancellationTokenSource token)
        {
            await this.wss.SubscribeToCandles(symbol, p);
        }

        protected override async Task ObserveCandles_Stop(string symbol, CandlePeriod p, CancellationTokenSource token)
        {
            await this.wss.UnSubscribeToCandles(symbol, p);
        }
    }
}
