using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class StrategyRunsDTO: DataDTOBase
    {
        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? StopTime { get; set; }

        public string StrategyDescription { get; set; }
        [Required]
        public StrategyType StrategyType { get; set; }
        [Required]
        public int StrategySettings_ID { get; set; }
        [Required]
        public int PlatformJob_ID { get; set; }
        public PlatformJobsDTO PlatformJob { get; set; }

        public virtual ICollection<StrategyLogsDTO> StrategyLog { get; set; }
    }
}
