using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model.Logs
{
    public class PlatformJob: AsUserModel
    {
        [Required]
        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        [InverseProperty("PlatformJob")]
        public virtual ICollection<PlatformLogs> PlatformLogs { get; set; }

        [InverseProperty("PlatformJob")]
        public virtual ICollection<StrategyRuns> StrategyRuns { get; set; }
    }
}
