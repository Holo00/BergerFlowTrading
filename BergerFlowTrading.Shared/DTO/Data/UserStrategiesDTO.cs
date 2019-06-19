using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class UserStrategiesDTO
    {
        [Required]
        public string UserId { get; set; }
        public List<DataDTOBase> Limit4Strategies { get; set; }
    }
}
