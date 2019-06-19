using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class StrategyLogsDTO: BaseLogDTO
    {
        [Required]
        public int StrategyRuns_ID { get; set; }
        public StrategyRunsDTO StrategyRuns { get; set; }
    }
}
