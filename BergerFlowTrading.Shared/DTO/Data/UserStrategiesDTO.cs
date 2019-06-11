using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class UserStrategiesDTO
    {
        public string UserId { get; set; }
        public List<DataDTOBase> Limit4Strategies { get; set; }
    }
}
