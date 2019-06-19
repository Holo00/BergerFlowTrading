using BergerFlowTrading.Model.Identity;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model
{
    public class LimitArbitrageStrategy4Settings : AsUserModel
    {
        [Required]
        public int Exchange_ID_1 { get; set; }
        [ForeignKey("Exchange_ID_1")]
        public virtual Exchange Exchange_1 { get; set; }

        public int Exchange_ID_2 { get; set; }
        [ForeignKey("Exchange_ID_2")]
        public virtual Exchange Exchange_2 { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool ManagementBalanceON { get; set; }

        [Required]
        public decimal MinATRValue { get; set; }

        [Required]
        public ValueCurrency Value_Currency { get; set; }

        [Required]
        public decimal Value_To_Trade_Min { get; set; }

        [Required]
        public decimal Value_To_Trade_Max { get; set; }

        [Required]
        public decimal Min_Price { get; set; }

        [Required]
        public decimal Max_Price { get; set; }

        [Required]
        public decimal BaseCurrency_Share_Percentage { get; set; }
    }
}
