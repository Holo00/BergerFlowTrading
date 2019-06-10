using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public class Balance : UniqueIDItem, ICloneable, ITemporalObject
    {
        public string Id { get; set; }
        public string Currency { get; set; }
        public decimal? Available { get; set; }
        public decimal? Reserved { get; set; }
        public decimal? Total { get; set; }
        public DateTime? LastUpdated { get; set; }

        public Balance()
        {

        }

        public Balance(string currency)
        {
            this.Currency = currency;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
