using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class LimitArbitrageStrategy4SettingsDTO: UserDataDTO, IStrategySettingDTO
    {
        [Required]
        public int Exchange_ID_1 { get; set; }
        [Required]
        public virtual ExchangeDTO Exchange_1 { get; set; }
        [Required]
        public int Exchange_ID_2 { get; set; }
        [Required]
        public virtual ExchangeDTO Exchange_2 { get; set; }
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
        [Required]
        public string StrategyName { get; set; }
        [Required]
        public StrategyType StrategyType { get; set; }
    }

    public enum ValueCurrency
    {
        Base,
        Quote,
        BTC,
        USD
    }
}
