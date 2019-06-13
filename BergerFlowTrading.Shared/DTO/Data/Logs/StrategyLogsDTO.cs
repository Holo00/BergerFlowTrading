using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class StrategyLogsDTO: BaseLogDTO
    {
        public int StrategyRuns_ID { get; set; }
        public StrategyRunsDTO StrategyRuns { get; set; }
    }
}
