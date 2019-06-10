using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public class Symbol : UniqueIDItem, ICloneable, ITemporalObject
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }

        public decimal? MakerFee { get; set; }
        public decimal? TakerFee { get; set; }
        public decimal? TickSize { get; set; }

        public int? basePrecision { get; set; }
        public int? quotePrecision { get; set; }

        public decimal? quantityIncrement { get; set; }
        public decimal? MinBTCValuePerOrder { get; set; }

        public DateTime? LastUpdated { get; set; }
        public decimal? LotSize { get; set; }
        public decimal? MinQty { get; set; }

        public bool CanExecuteToday { get; set; }

        public string StatusMessage { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
