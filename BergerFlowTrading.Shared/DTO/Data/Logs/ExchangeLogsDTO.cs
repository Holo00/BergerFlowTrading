using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class ExchangeLogsDTO: BaseLogDTO
    {
        [Required]
        public int PlatformJob_ID { get; set; }
        public PlatformJobsDTO PlatformJob { get; set; }

        [Required]
        public int Exchange_ID { get; set; }

        public virtual ExchangeDTO Exchange { get; set; }

        public string Symbol { get; set; }
    }
}
