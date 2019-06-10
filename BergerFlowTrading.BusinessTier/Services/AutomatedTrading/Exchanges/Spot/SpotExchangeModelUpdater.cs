using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public class SpotExchangeModelUpdater
    {
        private List<ExchangeModel> models { get; set; }
        private ExchangeName Exchange { get; set; }

        public SpotExchangeModelUpdater(List<ExchangeModel> models, ExchangeName Exchange)
        {
            this.models = models;
            this.Exchange = Exchange;
        }

        public void UpdateBalance(Balance b)
        {
            List<ExchangeModel> modelsToUpdate = models.Where(x => x.Symbol.Value.BaseCurrency == b.Currency || x.Symbol.Value.QuoteCurrency == b.Currency).ToList();

            foreach (ExchangeModel m in modelsToUpdate)
            {
                _UpdateBalance(b, m);
            }
        }

        private void _UpdateBalance(Balance b, ExchangeModel m)
        {
            if (m.BaseBalance == null && m.Symbol.Value.BaseCurrency == b.Currency)
            {
                m.BaseBalance = new Observable<Balance>(b);
            }
            else if (m.BaseBalance.Value.Currency == b.Currency)
            {
                m.BaseBalance.Update(b);
            }

            if (m.QuoteBalance == null && m.Symbol.Value.QuoteCurrency == b.Currency)
            {
                m.QuoteBalance = new Observable<Balance>(b);
            }
            else if (m.QuoteBalance.Value.Currency == b.Currency)
            {
                m.QuoteBalance.Update(b);
            }
        }

        public void UpdateCandles(List<Candle> c, CandlePeriod p, string symbol)
        {
            ExchangeModel toUpdate = models.FirstOrDefault(x => x.Symbol.Value.symbol == symbol);

            if (p == CandlePeriod.M1)
            {
                this.UpdateCandles_M1(c, toUpdate);
            }
            else if (p == CandlePeriod.M5)
            {
                this.UpdateCandles_M5(c, toUpdate);
            }
            else if (p == CandlePeriod.M30)
            {
                this.UpdateCandles_M30(c, toUpdate);
            }
            else if (p == CandlePeriod.H1)
            {
                this.UpdateCandles_H1(c, toUpdate);
            }
            else if (p == CandlePeriod.H4)
            {
                this.UpdateCandles_H4(c, toUpdate);
            }
            else if (p == CandlePeriod.D1)
            {
                this.UpdateCandles_D1(c, toUpdate);
            }
            else if (p == CandlePeriod.W1)
            {
                this.UpdateCandles_W1(c, toUpdate);
            }
        }

        private void UpdateCandles_M1(List<Candle> c, ExchangeModel toUpdate)
        {
            if (toUpdate.CandlesM1 == null)
            {
                toUpdate.CandlesM1 = new ObservableList<Candle>(c);
            }
            else
            {
                foreach (Candle c1 in c)
                {
                    toUpdate.CandlesM1.Update(c1);
                }
            }
        }

        private void UpdateCandles_M5(List<Candle> c, ExchangeModel toUpdate)
        {
            if (toUpdate.CandlesM5 == null)
            {
                toUpdate.CandlesM5 = new ObservableList<Candle>(c);
            }
            else
            {
                foreach (Candle c1 in c)
                {
                    toUpdate.CandlesM5.Update(c1);
                }
            }
        }

        private void UpdateCandles_M30(List<Candle> c, ExchangeModel toUpdate)
        {
            if (toUpdate.CandlesM30 == null)
            {
                toUpdate.CandlesM30 = new ObservableList<Candle>(c);
            }
            else
            {
                foreach (Candle c1 in c)
                {
                    toUpdate.CandlesM30.Update(c1);
                }
            }
        }

        private void UpdateCandles_H1(List<Candle> c, ExchangeModel toUpdate)
        {
            if (toUpdate.CandlesH1 == null)
            {
                toUpdate.CandlesH1 = new ObservableList<Candle>(c);
            }
            else
            {
                foreach (Candle c1 in c)
                {
                    toUpdate.CandlesH1.Update(c1);
                }
            }
        }

        private void UpdateCandles_H4(List<Candle> c, ExchangeModel toUpdate)
        {
            if (toUpdate.CandlesH4 == null)
            {
                toUpdate.CandlesH4 = new ObservableList<Candle>(c);
            }
            else
            {
                foreach (Candle c1 in c)
                {
                    toUpdate.CandlesH4.Update(c1);
                }
            }
        }

        private void UpdateCandles_D1(List<Candle> c, ExchangeModel toUpdate)
        {
            if (toUpdate.CandlesD1 == null)
            {
                toUpdate.CandlesD1 = new ObservableList<Candle>(c);
            }
            else
            {
                foreach (Candle c1 in c)
                {
                    toUpdate.CandlesD1.Update(c1);
                }
            }
        }

        private void UpdateCandles_W1(List<Candle> c, ExchangeModel toUpdate)
        {
            if (toUpdate.CandlesW1 == null)
            {
                toUpdate.CandlesW1 = new ObservableList<Candle>(c);
            }
            else
            {
                foreach (Candle c1 in c)
                {
                    toUpdate.CandlesW1.Update(c1);
                }
            }
        }

        public void UpdateOrderbooks(List<OrderbookRecord> o, string symbol, bool dropCreate = false)
        {
            ExchangeModel toUpdate = models.FirstOrDefault(x => x.Symbol.Value.symbol == symbol);

            List<OrderbookRecord> bids = o.Where(x => x.Side == Side.Buy).ToList();
            List<OrderbookRecord> asks = o.Where(x => x.Side == Side.Sell).ToList();

            if (dropCreate)
            {
                toUpdate.OrderbookL2Bids.Value.Clear();
                toUpdate.OrderbookL2Asks.Value.Clear();
                toUpdate.OrderbookL2Bids = new ObservableList<OrderbookRecord>(bids);
                toUpdate.OrderbookL2Asks = new ObservableList<OrderbookRecord>(asks);
                toUpdate.LastOrderbookUpdateWS = DateTime.UtcNow;
            }
            else if (toUpdate != null && o != null && o.Any())
            {
                foreach (OrderbookRecord record in o)
                {
                    this.UpdateOrderbookRecord(symbol, record, toUpdate);
                }

                //remove 0
                if (toUpdate.OrderbookL2Asks.Value != null && toUpdate.OrderbookL2Asks.Value.Any())
                {
                    List<OrderbookRecord> zeroAsks = toUpdate.OrderbookL2Asks.Value.Where(x => x.Quantity == 0).ToList();

                    foreach (OrderbookRecord m in zeroAsks)
                    {
                        toUpdate.OrderbookL2Asks.Value.Remove(m);
                    }
                }

                if (toUpdate.OrderbookL2Bids.Value != null && toUpdate.OrderbookL2Bids.Value.Any())
                {
                    List<OrderbookRecord> zeroBids = toUpdate.OrderbookL2Bids.Value.Where(x => x.Quantity == 0).ToList();

                    foreach (OrderbookRecord m in zeroBids)
                    {
                        toUpdate.OrderbookL2Bids.Value.Remove(m);
                    }
                }

                toUpdate.LastOrderbookUpdate = DateTime.UtcNow;
            }
        }

        private void UpdateOrderbookRecord(string symbol, OrderbookRecord orderBookRecordModel, ExchangeModel toUpdate, int attemps = 3)
        {
            try
            {
                OrderbookRecord toUpdateRecord = null;

                if (orderBookRecordModel.Side == Side.Buy)
                {
                    var arr = toUpdate.OrderbookL2Bids?.Value.ToArray();
                    toUpdateRecord = arr.FirstOrDefault(x => x?.Price == orderBookRecordModel.Price);

                    if (toUpdateRecord != null)
                    {
                        if (toUpdateRecord.Sequence == null || toUpdateRecord.Sequence < orderBookRecordModel.Sequence)
                        {
                            if (orderBookRecordModel.Quantity == 0)
                            {
                                //Remove the record

                                toUpdate.OrderbookL2Bids.Remove(toUpdateRecord);
                            }
                            else
                            {
                                //Update
                                toUpdate.OrderbookL2Bids.Update(orderBookRecordModel);
                            }
                        }
                    }
                    else
                    {
                        //Add new
                        toUpdate.OrderbookL2Bids.Add(orderBookRecordModel);
                    }
                }
                else if (orderBookRecordModel.Side == Side.Sell)
                {
                    var arr = toUpdate.OrderbookL2Asks?.Value.ToArray();
                    toUpdateRecord = arr.FirstOrDefault(x => x?.Price == orderBookRecordModel.Price);

                    if (toUpdateRecord != null)
                    {
                        if (toUpdateRecord.Sequence == null || toUpdateRecord.Sequence < orderBookRecordModel.Sequence)
                        {
                            if (orderBookRecordModel.Quantity == 0)
                            {
                                //Remove the record
                                toUpdate.OrderbookL2Asks.Remove(toUpdateRecord);
                            }
                            else
                            {
                                //Update
                                toUpdate.OrderbookL2Asks.Remove(orderBookRecordModel);

                            }
                        }
                    }
                    else
                    {
                        //Add new
                        toUpdate.OrderbookL2Asks.Add(orderBookRecordModel);
                    }
                }
            }
            catch (Exception ex)
            {
                attemps--;

                if (attemps > 0)
                {
                    this.UpdateOrderbookRecord(symbol, orderBookRecordModel, toUpdate, attemps);
                }
                else
                {
                    throw;
                }
            }
        }

        public void UpdateOrders(List<Order> orderModels, string symbol)
        {
            ExchangeModel toUpdate = models.FirstOrDefault(x => x.Symbol.Value.symbol == symbol);

            if (orderModels != null && orderModels.Any())
            {
                foreach (Order order in orderModels)
                {
                    this.UpdateOrder(order, toUpdate);
                    this.CheckSymbolOrderFills(order, toUpdate);
                }
            }
        }

        private void CheckSymbolOrderFills(Order orderModel, ExchangeModel toUpdate)
        {
            try
            {
                if ((orderModel.CreatedAt != null && orderModel.CreatedAt > toUpdate.StartTime
                    && (orderModel.Status == OrderStatus.PartiallyFilled || orderModel.Status == OrderStatus.Filled))
                    || orderModel.OrderType == OrderType.Market)
                {
                    toUpdate.TradesChanged = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateOrder(Order orderModel, ExchangeModel toUpdate, int attemps = 3)
        {
            try
            {
                Order orderToUpdate = null;

                if (toUpdate != null)
                {
                    if (orderModel.Side == Side.Buy)
                    {
                        if (toUpdate?.OrdersBids?.Value != null)
                        {
                            var arr = toUpdate.OrdersBids?.Value.ToArray();

                            if (arr != null)
                            {
                                orderToUpdate = arr?.FirstOrDefault(x => (orderModel.ClientOrderId != null && x.ClientOrderId == orderModel.ClientOrderId)
                                                        || (orderModel.Id != null && x.Id == orderModel.Id));
                            }
                            else
                            {
                                orderToUpdate = null;
                            }
                        }

                        if (orderToUpdate != null)
                        {
                            toUpdate.OrdersBids.Remove(orderToUpdate);
                        }

                        toUpdate.OrdersBids.Value.Add(orderModel);
                    }
                    else if (orderModel.Side == Side.Sell)
                    {
                        if (toUpdate?.OrdersAsks?.Value != null)
                        {
                            var arr = toUpdate.OrdersAsks?.Value.ToArray();

                            if (arr != null)
                            {
                                orderToUpdate = arr?.FirstOrDefault(x => (orderModel.ClientOrderId != null && x.ClientOrderId == orderModel.ClientOrderId)
                                                                                    || (orderModel.Id != null && x.Id == orderModel.Id));
                            }
                            else
                            {
                                orderToUpdate = null;
                            }
                        }

                        if (orderToUpdate != null)
                        {
                            toUpdate.OrdersAsks.Remove(orderToUpdate);
                        }

                        toUpdate.OrdersAsks.Value.Add(orderModel);
                    }
                }
            }
            catch (Exception ex)
            {
                attemps--;

                if (attemps > 0)
                {
                    this.UpdateOrder(orderModel, toUpdate, attemps);
                }
                else
                {
                    throw;
                }
            }
        }


        public void UpdateOwnTardes(List<Trade> trades, string symbol)
        {
            ExchangeModel toUpdate = models.FirstOrDefault(x => x.Symbol.Value.symbol == symbol);

            foreach (Trade t in trades)
            {
                this.UpdateOwnTrade(t, toUpdate);
            }
        }

        private void UpdateOwnTrade(Trade t, ExchangeModel toUpdate)
        {
            if (toUpdate != null)
            {
                Trade toUpdateTrade = toUpdate.MyTrades.Value.FirstOrDefault(x => x.Id == t.Id && x.Side == t.Side && x.Exchange == t.Exchange);

                if (toUpdateTrade == null && t.TimeStamp > toUpdate.StartTime)
                {
                    toUpdate.MyTrades.Add((Trade)t.Clone());
                }
            }
        }


        public void UpdateSymbol(Symbol s, string symbol)
        {
            ExchangeModel toUpdate = models.FirstOrDefault(x => x.Symbol.Value.symbol == symbol);

            if (toUpdate != null)
            {
                toUpdate.Symbol.Update(s);
            }
            else
            {
                this.models.Add(new ExchangeModel(this.Exchange, s));
            }
        }


        public void UpdateSymbols(List<Symbol> symbolModels)
        {
            foreach (Symbol symbol in symbolModels)
            {
                this.UpdateSymbol(symbol, symbol.symbol);
            }
        }


        public void UpdateTrades(List<Trade> trades, string symbol, bool DropCreate = false)
        {
            ExchangeModel toUpdate = models.FirstOrDefault(x => x.Symbol.Value.symbol == symbol);

            if (toUpdate != null && trades != null && trades.Any())
            {
                if (DropCreate)
                {
                    toUpdate.Trades.Value = trades;
                }
                else
                {
                    foreach (Trade trade in trades)
                    {
                        this.UpdateTrade(symbol, trade, toUpdate);
                    }
                }
            }
        }

        private void UpdateTrade(string symbol, Trade tradeModel, ExchangeModel toUpdate)
        {
            Trade tradeToUpdate = toUpdate.Trades.Value.FirstOrDefault(x => x.Id == tradeModel.Id);

            if (tradeToUpdate == null)
            {
                toUpdate.Trades.Add(tradeModel);
            }
        }

        public void UpdateBalances(List<Balance> bals)
        {
            foreach (Balance b in bals)
            {
                this.UpdateBalance(b);
            }
        }
    }
}
