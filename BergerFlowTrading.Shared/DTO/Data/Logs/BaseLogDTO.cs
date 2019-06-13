using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data.Logs
{
    public class BaseLogDTO: DataDTOBase
    {
        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public int LineNumber { get; set; }

        [Required]
        public eventType eventType { get; set; }

        public string DetailLevelKeyword { get; set; }

        public string ExceptionMessage { get; set; }

        public string StackTrace { get; set; }

        public string InnerException { get; set; }
    }

    public enum eventType
    {
        Info,
        Warning,
        Exception
    }
}
