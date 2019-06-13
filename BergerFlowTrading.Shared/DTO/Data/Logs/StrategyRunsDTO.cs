using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class StrategyRunsDTO: DataDTOBase
    {
        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        public string StrategyDescription { get; set; }

        public StrategyType StrategyType { get; set; }

        public int StrategySettings_ID { get; set; }

        public int PlatformJob_ID { get; set; }
        public PlatformJobsDTO PlatformJob { get; set; }

        public virtual ICollection<StrategyLogsDTO> StrategyLog { get; set; }
    }
}
