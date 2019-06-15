using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class PlatformJobsDTO: UserDataDTO
    {
        [Required]
        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        public virtual ICollection<PlatformLogsDTO> PlatformLogs { get; set; }

        public virtual ICollection<StrategyRunsDTO> StrategyRuns { get; set; }
    }
}
