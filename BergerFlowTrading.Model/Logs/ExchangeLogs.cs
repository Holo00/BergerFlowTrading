using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model.Logs
{
    public class ExchangeLogs: BaseLog
    {
        [Required]
        public int PlatformJob_ID { get; set; }
        [ForeignKey("PlatformJob_ID")]
        public PlatformJob PlatformJob { get; set; }

        [Required]
        public int Exchange_ID { get; set; }

        [ForeignKey("Exchange_ID")]
        public virtual Exchange Exchange { get; set; }

        public string Symbol { get; set; }
    }
}
