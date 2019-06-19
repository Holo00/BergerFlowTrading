using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class ExchangeDTO : DataDTOBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ApiUrl { get; set; }
        [Required]
        public string WSS_Url { get; set; }
        [Required]
        public int ApiTimeoutMilliseconds { get; set; }
        [Required]
        public int? DelayBetweenCallMilliseonds { get; set; }
        [Required]
        public string FacadeClassName { get; set; }
        [Required]
        public int RateMaxQuantity { get; set; }
        [Required]
        public int RateLimitIntervalSeconds { get; set; }


        public virtual ICollection<ExchangeCustomSettingsDTO> ExchangeCustomSettings { get; set; }
        public virtual ICollection<UserExchangeSecretDTO> UserExchangeSecrets { get; set; }
        public virtual ICollection<LimitArbitrageStrategy4SettingsDTO> LimitStrategy4Settings1 { get; set; }
        public virtual ICollection<LimitArbitrageStrategy4SettingsDTO> LimitStrategy4Settings2 { get; set; }
    }
}
