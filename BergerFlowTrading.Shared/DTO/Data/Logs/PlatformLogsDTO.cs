using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class PlatformLogsDTO: BaseLogDTO
    {
        [Required]
        public int PlatformJob_ID { get; set; }
        public PlatformJobsDTO PlatformJob { get; set; }
    }
}
