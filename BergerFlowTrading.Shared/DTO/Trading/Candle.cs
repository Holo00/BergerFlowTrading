using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public class Candle : UniqueIDItem, ITemporalObject, ICloneable
    {
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        public decimal? High { get; set; }

        public decimal? Low { get; set; }

        public decimal? Volume { get; set; }
        public decimal? VolumeQuote { get; set; }
        public DateTime? TimeStamp { get; set; }
        public DateTime? LastUpdated { get; set; }
        public CandlePeriod CandlePeriod { get; set; }

        public string Symbol { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public enum CandlePeriod
    {
        M1,
        M5,
        M15,
        M30,
        H1,
        H4,
        D1,
        W1,
        Undefined
    }
}
