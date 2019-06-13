using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class PlatformLogsDTO: BaseLogDTO
    {
        public int PlatformJob_ID { get; set; }
        public PlatformJobsDTO PlatformJob { get; set; }
    }
}
