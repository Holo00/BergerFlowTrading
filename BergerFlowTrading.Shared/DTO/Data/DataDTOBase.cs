using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class DataDTOBase
    {
        public int? ID { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
    }

    public class UserDataDTO : DataDTOBase
    {
        public string User_ID { get; set; }
        public virtual UserStateDTO User { get; set; }
    }
}
