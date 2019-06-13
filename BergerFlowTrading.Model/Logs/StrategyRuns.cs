using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model.Logs
{
    public class StrategyRuns: BaseModel
    {
        [Required]
        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        [Required]
        public string StrategyDescription { get; set; }

        [Required]
        public StrategyType StrategyType { get; set; }

        [Required]
        public int StrategySettings_ID { get; set; }

        public int PlatformJob_ID { get; set; }
        [ForeignKey("PlatformJob_ID")]
        public PlatformJob PlatformJob { get; set; }

        [InverseProperty("StrategyRuns")]
        public virtual ICollection<StrategyLog> StrategyLog { get; set; }
    }
}
