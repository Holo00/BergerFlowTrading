using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class DataDTOBase
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
        [Required]
        public DateTime CreatedTimeStamp { get; set; }
        [Required]
        public DateTime UpdatedTimeStamp { get; set; }
    }

    public class UserDataDTO : DataDTOBase
    {
        [Required]
        public string User_ID { get; set; }
        public virtual UserStateDTO User { get; set; }
    }
}
