using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model.Logs
{
    public class StrategyLog : BaseLog
    {
        public int StrategyRuns_ID { get; set; }
        [ForeignKey("StrategyRuns_ID")]
        public StrategyRuns StrategyRuns { get; set; }
    }

}
