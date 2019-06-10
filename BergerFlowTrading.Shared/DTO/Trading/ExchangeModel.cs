using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public class ExchangeModel
    {
        public ExchangeName Exchange { get; private set; }

        public SemaphoreSlim tradeSem { get; set; }
        public bool TradesChanged { get; set; }
        public ObservableList<Trade> MyTrades { get; set; }
        public DateTime StartTime { get; private set; }

        public Observable<Symbol> Symbol { get; set; }

        public ObservableList<Order> OrdersBids { get; set; }
        public ObservableList<Order> OrdersAsks { get; set; }

        public ObservableList<Candle> CandlesM1 { get; set; }
        public ObservableList<Candle> CandlesM5 { get; set; }
        public ObservableList<Candle> CandlesM15 { get; set; }
        public ObservableList<Candle> CandlesM30 { get; set; }
        public ObservableList<Candle> CandlesH1 { get; set; }
        public ObservableList<Candle> CandlesH4 { get; set; }
        public ObservableList<Candle> CandlesD1 { get; set; }
        public ObservableList<Candle> CandlesW1 { get; set; }

        public ObservableList<OrderbookRecord> OrderbookL2Bids { get; set; }
        public ObservableList<OrderbookRecord> OrderbookL2Asks { get; set; }

        public ObservableList<Trade> Trades { get; set; }

        public Observable<Balance> BaseBalance { get; set; }
        public Observable<Balance> QuoteBalance { get; set; }

        public DateTime? LastDealtOrderRefresh { get; set; }

        public OrderbookRecord OrderbookL1Bid
        {
            get
            {
                var v = OrderbookL2Bids.Value;

                if (v != null && v.Any())
                {
                    var arr = v.ToArray();
                    return arr.OrderByDescending(x => x.Price).FirstOrDefault();
                }

                return null;
            }
        }

        public OrderbookRecord OrderbookL1Ask
        {
            get
            {
                var v = OrderbookL2Asks.Value;

                if (v != null && v.Any())
                {
                    var arr = v.ToArray();
                    return arr.OrderBy(x => x.Price).FirstOrDefault();
                }

                return null;
            }
        }

        public DateTime? LastOrderbookUpdate { get; set; }
        public DateTime? LastOrderbookUpdateWS { get; set; }

        public ExchangeModel(ExchangeName Exchange, Symbol symbolModel)
        {
            this.LastOrderbookUpdateWS = DateTime.UtcNow;

            this.StartTime = DateTime.UtcNow;
            this.tradeSem = new SemaphoreSlim(1, 1);
            this.MyTrades = new ObservableList<Trade>(new List<Trade>());

            this.Exchange = Exchange;

            this.Symbol = new Observable<Symbol>(symbolModel);

            this.OrdersBids = new ObservableList<Order>(new List<Order>());
            this.OrdersAsks = new ObservableList<Order>(new List<Order>());

            this.CandlesM1 = new ObservableList<Candle>(new List<Candle>());
            this.CandlesM5 = new ObservableList<Candle>(new List<Candle>());
            this.CandlesM15 = new ObservableList<Candle>(new List<Candle>());
            this.CandlesM30 = new ObservableList<Candle>(new List<Candle>());
            this.CandlesH1 = new ObservableList<Candle>(new List<Candle>());
            this.CandlesH4 = new ObservableList<Candle>(new List<Candle>());
            this.CandlesD1 = new ObservableList<Candle>(new List<Candle>());
            this.CandlesW1 = new ObservableList<Candle>(new List<Candle>());

            this.OrderbookL2Bids = new ObservableList<OrderbookRecord>(new List<OrderbookRecord>());
            this.OrderbookL2Asks = new ObservableList<OrderbookRecord>(new List<OrderbookRecord>());

            this.Trades = new ObservableList<Trade>(new List<Trade>());

            this.BaseBalance = new Observable<Balance>(new Balance(this.Symbol.Value.BaseCurrency));
            this.QuoteBalance = new Observable<Balance>(new Balance(this.Symbol.Value.QuoteCurrency));

            this.LastDealtOrderRefresh = DateTime.UtcNow.AddDays(-1);
        }


        public decimal? MarketBuyAveragePrice(decimal Quantity)
        {
            decimal? cost = this.MarketBuyTotalCost(Quantity).Item1;

            if (cost == null || cost == 0)
            {
                return null;
            }
            else
            {
                return cost / Quantity;
            }
        }

        public decimal? MarketSellAveragePrice(decimal Quantity)
        {
            decimal? returns = this.MarketSellTotalReturn(Quantity).Item1;

            if (returns == null || returns == 0)
            {
                return null;
            }
            else
            {
                return returns / Quantity;
            }
        }

        public (decimal?, List<ExpectedExecution>) MarketBuyQuantity(decimal QuantityQuote, int attemps = 3)
        {
            List<ExpectedExecution> exec = new List<ExpectedExecution>();

            try
            {
                decimal qty = 0;

                if (OrderbookL2Asks?.Value != null && OrderbookL2Asks.Value.Any() && QuantityQuote > 0)
                {
                    decimal QuantityQuoteLeft = QuantityQuote;
                    var arr = this.OrderbookL2Asks.Value.Where(x => x != null).ToArray();

                    if (arr != null && arr.Any())
                    {
                        List<OrderbookRecord> books = arr.OrderBy(x => x.Price).ToList();

                        int i = 0;

                        while (i < books.Count)
                        {
                            try
                            {
                                if (books[i].Quantity * books[i].Price.Value > QuantityQuoteLeft)
                                {
                                    exec.Add(new ExpectedExecution()
                                    {
                                        Symbol = this.Symbol.Value.symbol,
                                        Price = books[i].Price.Value,
                                        Quantity = QuantityQuoteLeft / books[i].Price.Value,
                                        Side = Side.Buy

                                    });

                                    qty += QuantityQuoteLeft / books[i].Price.Value;
                                    QuantityQuoteLeft = 0;
                                    break;
                                }
                                else
                                {
                                    exec.Add(new ExpectedExecution()
                                    {
                                        Symbol = this.Symbol.Value.symbol,
                                        Price = books[i].Price.Value,
                                        Quantity = books[i].Quantity.Value,
                                        Side = Side.Buy

                                    });

                                    qty += books[i].Quantity.Value;
                                    QuantityQuoteLeft -= books[i].Quantity.Value * books[i].Price.Value;
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            i++;
                        }
                    }

                    if (qty == 0 || QuantityQuoteLeft > 0)
                    {
                        return new ValueTuple<decimal?, List<ExpectedExecution>>(null, exec);
                    }
                    else
                    {
                        return new ValueTuple<decimal?, List<ExpectedExecution>>(qty, exec);
                    }
                }
                else
                {
                    return new ValueTuple<decimal?, List<ExpectedExecution>>(new decimal(0), exec);
                }
            }
            catch (Exception ex)
            {
                attemps--;

                if (attemps > 0)
                {
                    return this.MarketBuyQuantity(QuantityQuote, attemps);
                }
                else
                {
                    throw ex;
                }
            }
        }

        public (decimal?, List<ExpectedExecution>) MarketBuyTotalCost(decimal Quantity, int attemps = 3)
        {
            List<ExpectedExecution> exec = new List<ExpectedExecution>();


            try
            {
                decimal cost = 0;

                if (OrderbookL2Asks?.Value != null && OrderbookL2Asks.Value.Any() && Quantity > 0)
                {
                    decimal QuantityLeft = Quantity;
                    var arr = this.OrderbookL2Asks.Value.Where(x => x != null).ToArray();

                    if (arr != null && arr.Any())
                    {
                        List<OrderbookRecord> books = arr.OrderBy(x => x.Price).ToList();

                        int i = 0;

                        while (i < books.Count)
                        {
                            try
                            {
                                if (books[i].Quantity > QuantityLeft)
                                {
                                    exec.Add(new ExpectedExecution()
                                    {
                                        Symbol = this.Symbol.Value.symbol,
                                        Price = books[i].Price.Value,
                                        Quantity = QuantityLeft,
                                        Side = Side.Buy

                                    });

                                    cost += QuantityLeft * books[i].Price.Value;
                                    QuantityLeft = 0;
                                    break;
                                }
                                else
                                {
                                    exec.Add(new ExpectedExecution()
                                    {
                                        Symbol = this.Symbol.Value.symbol,
                                        Price = books[i].Price.Value,
                                        Quantity = books[i].Quantity.Value,
                                        Side = Side.Buy

                                    });

                                    cost += books[i].Quantity.Value * books[i].Price.Value;
                                    QuantityLeft -= books[i].Quantity.Value;
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            i++;
                        }
                    }

                    if (cost == 0 || QuantityLeft > 0)
                    {
                        return new ValueTuple<decimal?, List<ExpectedExecution>>(null, exec);
                    }
                    else
                    {
                        return new ValueTuple<decimal?, List<ExpectedExecution>>(cost, exec);
                    }
                }
                else
                {
                    return new ValueTuple<decimal?, List<ExpectedExecution>>(new decimal(0), exec);
                }
            }
            catch (Exception ex)
            {
                attemps--;

                if (attemps > 0)
                {
                    return this.MarketBuyTotalCost(Quantity, attemps);
                }
                else
                {
                    throw ex;
                }
            }
        }

        public (decimal?, List<ExpectedExecution>) MarketSellTotalReturn(decimal Quantity, int attemps = 3)
        {
            List<ExpectedExecution> exec = new List<ExpectedExecution>();

            try
            {
                decimal returns = 0;

                if (OrderbookL2Bids?.Value != null && OrderbookL2Bids.Value.Any() && Quantity > 0)
                {
                    decimal QuantityLeft = Quantity;
                    var arr = this.OrderbookL2Bids.Value.Where(x => x != null).ToArray();

                    if (arr != null && arr.Any())
                    {
                        List<OrderbookRecord> books = arr.OrderByDescending(x => x.Price).ToList();

                        int i = 0;

                        while (i < books.Count)
                        {
                            try
                            {
                                if (books[i].Quantity > QuantityLeft)
                                {
                                    exec.Add(new ExpectedExecution()
                                    {
                                        Symbol = this.Symbol.Value.symbol,
                                        Price = books[i].Price.Value,
                                        Quantity = QuantityLeft,
                                        Side = Side.Sell

                                    });

                                    returns += QuantityLeft * books[i].Price.Value;
                                    QuantityLeft = 0;
                                    break;
                                }
                                else
                                {
                                    exec.Add(new ExpectedExecution()
                                    {
                                        Symbol = this.Symbol.Value.symbol,
                                        Price = books[i].Price.Value,
                                        Quantity = books[i].Quantity.Value,
                                        Side = Side.Sell

                                    });

                                    returns += books[i].Quantity.Value * books[i].Price.Value;
                                    QuantityLeft -= books[i].Quantity.Value;
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            i++;
                        }

                    }

                    if (returns == 0 || QuantityLeft > 0)
                    {
                        return new ValueTuple<decimal?, List<ExpectedExecution>>(null, exec);
                    }
                    else
                    {
                        return new ValueTuple<decimal?, List<ExpectedExecution>>(returns, exec);
                    }
                }
                else
                {
                    return new ValueTuple<decimal?, List<ExpectedExecution>>(new decimal(0), exec);
                }
            }
            catch (Exception ex)
            {
                attemps--;

                if (attemps > 0)
                {
                    return this.MarketSellTotalReturn(Quantity, attemps);
                }
                else
                {
                    throw ex;
                }
            }
        }

        public decimal? GetMarketBuyPrice(decimal Quantity)
        {
            Dictionary<decimal, decimal> dic = this.GetPricesAndQuantitesForMarketOrder(Side.Buy, Quantity);

            if (dic.Any())
            {
                return dic.Max(x => x.Key);
            }
            else
            {
                return null;
            }
        }

        public decimal? GetMarketSellPrice(decimal Quantity)
        {
            Dictionary<decimal, decimal> dic = this.GetPricesAndQuantitesForMarketOrder(Side.Sell, Quantity);

            if (dic.Any())
            {
                return dic.Min(x => x.Key);
            }
            else
            {
                return null;
            }
        }


        public Dictionary<decimal, decimal> GetPricesAndQuantitesForMarketOrder(Side Side, decimal Quantity)
        {
            Dictionary<decimal, decimal> dic = new Dictionary<decimal, decimal>();

            decimal QuantityLeft = Quantity;

            if (Side == Side.Buy)
            {
                if (OrderbookL2Asks != null && Quantity > 0)
                {
                    List<OrderbookRecord> books = this.OrderbookL2Asks.Value.OrderBy(x => x.Price).ToList();

                    foreach (OrderbookRecord record in books)
                    {
                        if (record.Quantity > QuantityLeft)
                        {
                            dic.Add(record.Price.Value, QuantityLeft);
                            QuantityLeft = 0;
                            break;
                        }
                        else
                        {
                            dic.Add(record.Price.Value, record.Quantity.Value);
                            QuantityLeft -= record.Quantity.Value;
                        }
                    }
                }
            }
            else
            {
                if (OrderbookL2Bids != null && Quantity > 0)
                {
                    List<OrderbookRecord> books = this.OrderbookL2Bids.Value.OrderByDescending(x => x.Price).ToList();

                    foreach (OrderbookRecord record in books)
                    {
                        if (record.Quantity > QuantityLeft)
                        {
                            dic.Add(record.Price.Value, QuantityLeft);
                            QuantityLeft = 0;
                            break;
                        }
                        else
                        {
                            dic.Add(record.Price.Value, record.Quantity.Value);
                            QuantityLeft -= record.Quantity.Value;
                        }
                    }
                }
            }

            return dic;
        }

        public decimal? MidPrice
        {
            get
            {
                return (this.OrderbookL1Ask != null && this.OrderbookL1Bid != null)
                        ? ((this.OrderbookL1Bid.Price + this.OrderbookL1Ask.Price) / 2)
                        : this.OrderbookL1Ask != null ? this.OrderbookL1Ask.Price : this.OrderbookL1Bid?.Price;
            }
        }

        public decimal? LastPrice
        {
            get
            {
                return (this.Trades != null && this.Trades.Value.Any())
                        ? this.Trades.Value.OrderByDescending(x => x.TimeStamp.Value).FirstOrDefault().Price
                        : null;
            }
        }

        public (decimal? vol, decimal? low, decimal? high, ExchangeName ex)? VolatilityPercentageRatio(int attemps = 2)
        {
            try
            {
                decimal? midPrice = this.MidPrice;
                List<Candle> candles = this.CandlesH4.Value.Where(x => x.TimeStamp > DateTime.UtcNow.AddDays(-1)).OrderByDescending(x => x.TimeStamp).ToList();

                decimal low;
                decimal high;

                if (candles.Count <= 1)
                {
                    candles = this.CandlesD1.Value;

                    if (midPrice == null || candles == null || candles.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        low = candles.OrderBy(x => x.LastUpdated.Value).FirstOrDefault().Low.Value;
                        high = candles.OrderBy(x => x.LastUpdated.Value).LastOrDefault().High.Value;
                    }
                }
                else if (midPrice == null || candles == null)
                {
                    return null;
                }
                else
                {
                    low = candles.Min(x => x.Low.Value);
                    high = candles.Max(x => x.High.Value);
                }

                decimal range = high - low;

                return new ValueTuple<decimal?, decimal?, decimal?, ExchangeName>(range / midPrice, low, high, this.Exchange);
            }
            catch (Exception ex)
            {
                attemps--;

                if (attemps > 0)
                {
                    return this.VolatilityPercentageRatio(attemps);
                }
                else
                {
                    throw;
                }
            }
        }
    }

    public class ExpectedExecution
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public Side Side { get; set; }
    }
}
