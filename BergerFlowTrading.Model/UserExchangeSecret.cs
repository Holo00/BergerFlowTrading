using BergerFlowTrading.Model.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model
{
    public class UserExchangeSecret : AsUserModel
    {
        [Required]
        public int Exchange_ID { get; set; }

        [ForeignKey("Exchange_ID")]
        public virtual Exchange Exchange { get; set; }


        [Required]
        public string Api_ID { get; set; }

        [Required]
        public string Api_Secret { get; set; }
    }
}
