using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class ExchangeLogsDTO: BaseLogDTO
    {
        public int PlatformJob_ID { get; set; }
        public PlatformJobsDTO PlatformJob { get; set; }

        public int Exchange_ID { get; set; }

        public virtual ExchangeDTO Exchange { get; set; }

        public string Symbol { get; set; }
    }
}
