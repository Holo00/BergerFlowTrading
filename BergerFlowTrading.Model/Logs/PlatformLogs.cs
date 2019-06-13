using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model.Logs
{
    public class PlatformLogs: BaseLog
    {
        public int PlatformJob_ID { get; set; }
        [ForeignKey("PlatformJob_ID")]
        public PlatformJob PlatformJob { get; set; }

    }
}
