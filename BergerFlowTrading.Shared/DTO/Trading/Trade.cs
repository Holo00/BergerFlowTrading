using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public class Trade : UniqueIDItem, ICloneable, ITemporalObject
    {
        public string Id { get; set; }
        public string symbol { get; set; }
        public string Exchange { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Commissions { get; set; }
        public string CommissionsAsset { get; set; }
        public Side Side { get; set; }
        public DateTime? TimeStamp { get; set; }
        public DateTime? LastUpdated { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
