using BergerFlowTrading.Model.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model
{
    public abstract class BaseModel
    {
        [Required]
        [Key]
        public virtual int? ID { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
        [Required]
        public DateTime CreatedTimeStamp { get; set; }
        [Required]
        public DateTime UpdatedTimeStamp { get; set; }
    }

    public abstract class AsUserModel : BaseModel
    {
        [Required]
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual AppUser User { get; set; }
    }
}
