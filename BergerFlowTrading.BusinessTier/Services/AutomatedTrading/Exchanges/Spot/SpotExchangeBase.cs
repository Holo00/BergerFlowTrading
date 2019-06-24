using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
using BergerFlowTrading.Shared.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public enum ExchangeCommunicationType
    {
        WebSocketsAndAPI,
        API_Only
    }

    public abstract class SpotExchangeBase : ISpotExchangeFacade
    {
        protected const string OBSERVED_KEY_SYMBOL = "OB_SYM";
        protected const string OBSERVED_KEY_SYMBOL_ALL = "OB_SYM_ALL";
        protected const string OBSERVED_KEY_ORDERS = "OB_ORDS";
        protected const string OBSERVED_KEY_ORDERBOOKS = "OB_BOOKS";
        protected const string OBSERVED_KEY_BALANCES = "OB_BAL";
        protected const string OBSERVED_KEY_BALANCES_ALL = "OB_BAL_ALL";
        protected const string OBSERVED_KEY_TRADES = "OB_TRADES";
        protected const string OBSERVED_KEY_OWNTRADES = "OB_OWNTRADES";
        protected const string OBSERVED_KEY_CANDLE_M1 = "OB_C_M1";
        protected const string OBSERVED_KEY_CANDLE_M5 = "OB_C_M5";
        protected const string OBSERVED_KEY_CANDLE_M30 = "OB_C_M30";
        protected const string OBSERVED_KEY_CANDLE_H1 = "OB_C_H1";
        protected const string OBSERVED_KEY_CANDLE_H4 = "OB_C_H4";
        protected const string OBSERVED_KEY_CANDLE_D1 = "OB_C_D1";
        protected const string OBSERVED_KEY_CANDLE_W1 = "OB_C_W1";

        protected List<(string key, string symbol, CancellationTokenSource source)> ObservedElements { get; set; }

        protected ISpotExchangeApi api { get; set; }

        protected IBodyReader reader { get; set; }

        protected SpotExchangeModelUpdater modelUpdater { get; set; }

        public List<ExchangeModel> exchangeModel { get; set; }
        public ExchangeName ExchangeName { get; protected set; }

        protected bool ObserveAllBalancesMode { get; set; }
        protected bool ObserveAllSymbolsMode { get; set; }

        protected ExchangeDTO exchangeSettings { get; set; }
        protected UserExchangeSecretDTO secrets { get; set; }
        protected ExchangeLogService logService { get; set; }



        public SpotExchangeBase(bool ObserveAllSymbolsMode
                                , bool ObserveAllBalancesMode
                                , ExchangeDTO exchangeSettings
                                , UserExchangeSecretDTO secrets
                                , ExchangeLogService logService
            )
        {
            this.exchangeSettings = exchangeSettings;
            this.secrets = secrets;

            this.ObservedElements = new List<(string key, string symbol, CancellationTokenSource source)>();
            this.exchangeModel = new List<ExchangeModel>();
            this.modelUpdater = new SpotExchangeModelUpdater(exchangeModel, this.ExchangeName);
            this.ObserveAllSymbolsMode = ObserveAllSymbolsMode;
            this.ObserveAllBalancesMode = ObserveAllBalancesMode;
            this.logService = logService;
        }

        protected virtual CancellationTokenSource Observe(string key, string symbol)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            this.ObservedElements.Add(new ValueTuple<string, string, CancellationTokenSource>(key, symbol, source));
            return source;
        }

        public virtual async Task StartObserveSymbol(string symbol)
        {
            if (this.ObserveAllSymbolsMode)
            {
                await this.StartObserveSymbol_All(symbol);
            }
            else
            {
                await this.StartObserveSymbol_One(symbol);
            }
        }


        public virtual async Task StartObserveBalance(string currency)
        {
            if (this.ObserveAllBalancesMode)
            {
                await this.StartObserveBalance_All(currency);
            }
            else
            {
                await this.StartObserveBalance_One(currency);
            }
        }


        protected abstract Task ObserveSymbol_All(CancellationTokenSource token);
        protected abstract Task ObserveSymbol_All_Stop(CancellationTokenSource token);

        protected abstract Task ObserveSymbol_One(string symbol, CancellationTokenSource token);
        protected abstract Task ObserveSymbol_One_Stop(string symbol, CancellationTokenSource token);

        protected abstract Task ObserveBalance_All(CancellationTokenSource token);
        protected abstract Task ObserveBalance_All_Stop(CancellationTokenSource token);

        protected abstract Task ObserveBalance_One(string currency, CancellationTokenSource token);
        protected abstract Task ObserveBalance_One_Stop(string currency, CancellationTokenSource token);

        protected abstract Task ObserveOrders(string symbol, CancellationTokenSource token);
        protected abstract Task ObserveOrders_Stop(string symbol, CancellationTokenSource token);

        protected abstract Task ObserveOrderbook(string symbol, CancellationTokenSource token);
        protected abstract Task ObserveOrderbook_Stop(string symbol, CancellationTokenSource token);

        protected abstract Task ObserveTrades(string symbol, CancellationTokenSource token);
        protected abstract Task ObserveTrades_Stop(string symbol, CancellationTokenSource token);

        protected abstract Task ObserveOwnTrades(string symbol, CancellationTokenSource token);
        protected abstract Task ObserveOwnTrades_Stop(string symbol, CancellationTokenSource token);

        protected abstract Task ObserveCandles(string symbol, CandlePeriod p, CancellationTokenSource token);
        protected abstract Task ObserveCandles_Stop(string symbol, CandlePeriod p, CancellationTokenSource token);


        protected virtual async Task StartObserveSymbol_All(string symbol)
        {
            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_SYMBOL_ALL))
            {
                CancellationTokenSource tokenSource = this.Observe(OBSERVED_KEY_SYMBOL_ALL, symbol);
                await this.ObserveSymbol_All(tokenSource);
            }
        }

        protected virtual async Task StartObserveSymbol_One(string symbol)
        {
            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_SYMBOL && x.symbol == symbol))
            {
                CancellationTokenSource tokenSource = this.Observe(OBSERVED_KEY_SYMBOL, symbol);
                await this.ObserveSymbol_One(symbol, tokenSource);
            }
        }

        protected virtual async Task StartObserveBalance_All(string currency)
        {
            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_BALANCES_ALL))
            {
                CancellationTokenSource tokenSource = this.Observe(OBSERVED_KEY_BALANCES_ALL, currency);
                await this.ObserveBalance_All(tokenSource);
            }
        }

        protected virtual async Task StartObserveBalance_One(string currency)
        {
            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_BALANCES && x.symbol == currency))
            {
                CancellationTokenSource tokenSource = this.Observe(OBSERVED_KEY_BALANCES, currency);
                await this.ObserveBalance_One(currency, tokenSource);
            }
        }


        protected virtual async Task StopObserveSymbol_All(string symbol)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == OBSERVED_KEY_SYMBOL_ALL && x.symbol == symbol);
            this.ObservedElements.Remove(el);

            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_SYMBOL_ALL))
            {
                await this.ObserveSymbol_All_Stop(el.source);
            }
        }


        protected virtual async Task StopObserveSymbol_One(string symbol)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == OBSERVED_KEY_SYMBOL && x.symbol == symbol);
            this.ObservedElements.Remove(el);
            await this.ObserveSymbol_One_Stop(symbol, el.source);
        }


        protected virtual async Task StopObserveBalance_All(string currency)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == OBSERVED_KEY_BALANCES_ALL && x.symbol == currency);
            this.ObservedElements.Remove(el);

            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_BALANCES_ALL))
            {
                await this.ObserveBalance_All_Stop(el.source);
            }
        }


        protected virtual async Task StopObserveBalance_One(string currency)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == OBSERVED_KEY_BALANCES && x.symbol == currency);
            this.ObservedElements.Remove(el);
            await this.ObserveBalance_One_Stop(currency, el.source);
        }



        public virtual async Task StopObserveSymbol(string symbol)
        {
            if (this.ObserveAllSymbolsMode)
            {
                await this.StopObserveSymbol_All(symbol);
            }
            else
            {
                await this.StopObserveSymbol_One(symbol);
            }
        }

        public virtual async Task StartObserveOrders(string symbol)
        {
            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_ORDERS && x.symbol == symbol))
            {
                CancellationTokenSource tokenSource = this.Observe(OBSERVED_KEY_ORDERS, symbol);
                await this.ObserveOrders(symbol, tokenSource);
            }
        }

        public virtual async Task StopObserveOrders(string symbol)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == OBSERVED_KEY_ORDERS && x.symbol == symbol);
            this.ObservedElements.Remove(el);
            await this.ObserveOrders_Stop(symbol, el.source);
        }

        public virtual async Task StartObserveOrderbooks(string symbol)
        {
            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_ORDERBOOKS && x.symbol == symbol))
            {
                CancellationTokenSource tokenSource = this.Observe(OBSERVED_KEY_ORDERBOOKS, symbol);
                await this.ObserveOrderbook(symbol, tokenSource);
            }
        }

        public virtual async Task StopObserveOrderbooks(string symbol)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == OBSERVED_KEY_ORDERBOOKS && x.symbol == symbol);
            this.ObservedElements.Remove(el);
            await this.ObserveOrderbook_Stop(symbol, el.source);
        }


        public virtual async Task StartObserveBalances(string currency)
        {
            if (this.ObserveAllBalancesMode)
            {
                await this.StartObserveBalance_All(currency);
            }
            else
            {
                await this.StartObserveBalance_One(currency);
            }
        }

        public virtual async Task StopObserveBalances(string currency)
        {
            if (this.ObserveAllSymbolsMode)
            {
                await this.StopObserveBalance_All(currency);
            }
            else
            {
                await this.StopObserveBalance_One(currency);
            }
        }

        public virtual async Task StartObserveTrades(string symbol)
        {
            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_TRADES && x.symbol == symbol))
            {
                CancellationTokenSource tokenSource = this.Observe(OBSERVED_KEY_TRADES, symbol);
                await this.ObserveTrades(symbol, tokenSource);
            }
        }

        public virtual async Task StopObserveTrades(string symbol)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == OBSERVED_KEY_TRADES && x.symbol == symbol);
            this.ObservedElements.Remove(el);
            await this.ObserveTrades_Stop(symbol, el.source);
        }

        public virtual async Task StartObserveOwnTrades(string symbol)
        {
            if (!this.ObservedElements.Any(x => x.key == OBSERVED_KEY_OWNTRADES && x.symbol == symbol))
            {
                CancellationTokenSource tokenSource = this.Observe(OBSERVED_KEY_OWNTRADES, symbol);
                await this.ObserveOwnTrades(symbol, tokenSource);
            }
        }

        public virtual async Task StopObserveOwnTrades(string symbol)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == OBSERVED_KEY_OWNTRADES && x.symbol == symbol);
            this.ObservedElements.Remove(el);
            await this.ObserveOwnTrades_Stop(symbol, el.source);
        }


        public virtual async Task StartObserveCandle(string symbol, CandlePeriod p)
        {
            if (!this.ObservedElements.Any(x => x.key == this.GetCandleKey(p) && x.symbol == symbol))
            {
                CancellationTokenSource tokenSource = this.Observe(this.GetCandleKey(p), symbol);
                await this.ObserveCandles(symbol, p, tokenSource);
            }
        }

        public virtual async Task StopObserveCandle(string symbol, CandlePeriod p)
        {
            var el = this.ObservedElements.FirstOrDefault(x => x.key == this.GetCandleKey(p) && x.symbol == symbol);
            this.ObservedElements.Remove(el);
            await this.ObserveCandles_Stop(symbol, p, el.source);
        }


        public virtual async Task RefreshBalance(string currency, bool force = false)
        {
            try
            {
                HttpResponseResult res = await this.api.GetBalance(currency, force);


                if (res.Code == HttpStatusCode.OK)
                {
                    Balance b = this.reader.ReadBalance_API(currency);
                    this.modelUpdater.UpdateBalance(b);
                }
                else
                {
                    throw new Exception($"Refresh balance failed:{Environment.NewLine}{currency}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }

        public virtual async Task RefreshCandles(string symbol, CandlePeriod p, int limit, bool force = false)
        {
            try
            {
                HttpResponseResult res = await this.api.GetCandles(symbol, p, limit, force);

                if (res.Code == HttpStatusCode.OK)
                {
                    List<Candle> c = this.reader.ReadCandles_API(res.body, p);
                    this.modelUpdater.UpdateCandles(c, p, symbol);
                }
                else
                {
                    throw new Exception($"Refresh canldes failed:{Environment.NewLine}{res.body}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }

        public virtual async Task RefreshOrderbooks(string symbol, bool force = false)
        {
            try
            {
                HttpResponseResult res = await this.api.GetOrderbooks(symbol, force);

                if (res.Code == HttpStatusCode.OK)
                {
                    List<OrderbookRecord> o = this.reader.ReadOrderbooks_API(res.body);
                    this.modelUpdater.UpdateOrderbooks(o, symbol);
                }
                else
                {
                    throw new Exception($"Refresh orderbooks failed:{Environment.NewLine}{res.body}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }

        public virtual async Task RefreshOrders(string symbol, bool force = false)
        {
            try
            {
                HttpResponseResult res = await this.api.GetOrders(symbol, force);

                if (res.Code == HttpStatusCode.OK)
                {
                    List<Order> o = this.reader.ReadOrders_API(res.body);
                    this.modelUpdater.UpdateOrders(o, symbol);
                }
                else
                {
                    throw new Exception($"Refresh orders failed:{Environment.NewLine}{res.body}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }

        public virtual async Task RefreshOwnTrades(string symbol, bool force = false)
        {
            try
            {
                HttpResponseResult res = await this.api.GetOwnTrades(symbol, force);

                if (res.Code == HttpStatusCode.OK)
                {
                    List<Trade> t = this.reader.ReadTrades_API(res.body);
                    this.modelUpdater.UpdateOwnTardes(t, symbol);
                }
                else
                {
                    throw new Exception($"Refresh own trade failed:{Environment.NewLine}{res.body}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }

        public virtual async Task RefreshSymbol(string symbol, bool force = false)
        {
            try
            {
                HttpResponseResult res = await this.api.GetSymbol(symbol, force);

                if (res.Code == HttpStatusCode.OK)
                {
                    Symbol s = this.reader.ReadSymbol_API(res.body);
                    this.modelUpdater.UpdateSymbol(s, symbol);
                }
                else
                {
                    throw new Exception($"Refresh symbol failed:{Environment.NewLine}{res.body}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }

        public virtual async Task RefreshTrades(string symbol, bool force = false)
        {
            try
            {
                HttpResponseResult res = await this.api.GetTrades(symbol, force);

                if (res.Code == HttpStatusCode.OK)
                {
                    List<Trade> t = this.reader.ReadTrades_API(res.body);
                    this.modelUpdater.UpdateTrades(t, symbol);
                }
                else
                {
                    throw new Exception($"Refresh symbol failed:{Environment.NewLine}{res.body}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }

        public virtual async Task LoadSymbols()
        {
            try
            {
                HttpResponseResult res = await this.api.GetSymbols();

                if (res.Code == HttpStatusCode.OK)
                {
                    List<Symbol> s = this.reader.ReadSymbols_API(res.body);
                    this.modelUpdater.UpdateSymbols(s);
                }
                else
                {
                    throw new Exception($"Load symbols failed:{Environment.NewLine}{res.body}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }



        public virtual async Task LoadBalances()
        {
            try
            {
                HttpResponseResult res = await this.api.GetBalances();

                if (res.Code == HttpStatusCode.OK)
                {
                    List<Balance> b = this.reader.ReadBalances_API(res.body);
                    this.modelUpdater.UpdateBalances(b);
                }
                else
                {
                    throw new Exception($"Load balances failed:{Environment.NewLine}{res.body}");
                }
            }
            catch (Exception ex)
            {
                this.logService.LogException(this.exchangeSettings.ID, null, ex);
            }
        }

        protected string GetCandleKey(CandlePeriod p)
        {
            string key = "";

            switch (p)
            {
                case CandlePeriod.M1:
                    key = OBSERVED_KEY_CANDLE_M1;
                    break;
                case CandlePeriod.M5:
                    key = OBSERVED_KEY_CANDLE_M5;
                    break;
                case CandlePeriod.M30:
                    key = OBSERVED_KEY_CANDLE_M30;
                    break;
                case CandlePeriod.H1:
                    key = OBSERVED_KEY_CANDLE_H1;
                    break;
                case CandlePeriod.H4:
                    key = OBSERVED_KEY_CANDLE_H4;
                    break;
                case CandlePeriod.D1:
                    key = OBSERVED_KEY_CANDLE_D1;
                    break;
                case CandlePeriod.W1:
                    key = OBSERVED_KEY_CANDLE_W1;
                    break;
            }

            return key;
        }

        public void Dispose()
        {
            if(!this.ObservedElements.Any())
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
