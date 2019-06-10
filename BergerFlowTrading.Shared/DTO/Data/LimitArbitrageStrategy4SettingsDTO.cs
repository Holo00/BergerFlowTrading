using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class LimitArbitrageStrategy4SettingsDTO: UserDataDTO
    {
        public int? Exchange_ID_1 { get; set; }
        public virtual ExchangeDTO Exchange_1 { get; set; }
        public int? Exchange_ID_2 { get; set; }
        public virtual ExchangeDTO Exchange_2 { get; set; }
        public string Symbol { get; set; }
        public bool Active { get; set; }
        public bool ManagementBalanceON { get; set; }
        public decimal MinATRValue { get; set; }
        public ValueCurrency Value_Currency { get; set; }
        public decimal Value_To_Trade_Min { get; set; }
        public decimal Value_To_Trade_Max { get; set; }
        public decimal Min_Price { get; set; }
        public decimal Max_Price { get; set; }
        public decimal BaseCurrency_Share_Percentage { get; set; }
    }

    public enum ValueCurrency
    {
        Base,
        Quote,
        BTC,
        USD
    }
}
