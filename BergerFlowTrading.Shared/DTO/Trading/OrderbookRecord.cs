using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public class OrderbookRecord : UniqueIDItem, ICloneable, ITemporalObject
    {
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public Side Side { get; set; }
        public long? Sequence { get; set; }
        public ExchangeName Exchange { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Symbol { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public enum Side
    {
        Buy,
        Sell
    }
}
