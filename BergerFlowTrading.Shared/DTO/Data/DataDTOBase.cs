using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class DataDTOBase
    {
        public int? ID { get; set; }
    }

    public class UserDataDTO : DataDTOBase
    {
        public string User_ID { get; set; }
        public virtual UserStateDTO User { get; set; }
    }
}
