using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BergerFlowTrading.Shared.DTO.Data.Logs;

namespace BergerFlowTrading.Model.Logs
{
    public abstract class BaseLog : BaseModel
    {
        public BaseLog()
        {

        }

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
}
