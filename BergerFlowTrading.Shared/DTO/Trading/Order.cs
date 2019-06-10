using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public class Order : UniqueIDItem, ICloneable, ITemporalObject
    {
        public string Id { get; set; }
        public string ClientOrderId { get; set; }
        public string Symbol { get; set; }
        public Side Side { get; set; }
        public OrderStatus Status { get; set; }
        public OrderType OrderType { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public decimal? Price { get; set; }
        public decimal? StopPrice { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? FilledQuantity { get; set; }
        public decimal? PendingQuantity { get; set; }
        public DateTime? ExpireTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastUpdated { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public enum TimeInForce
    {
        GoodTillCancel,
        ImmediateOrCancel,
        FillOrKill,
        GoodTillSpecTime
    }

    public enum OrderType
    {
        Market,
        Limit,
        StopLimit,
        StopMarket
    }

    public enum OrderStatus
    {
        New,
        Suspended,
        PartiallyFilled,
        Filled,
        Canceled,
        Expired,
        NA
    }
}
