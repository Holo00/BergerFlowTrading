using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class ExchangeDTO : DataDTOBase
    {
        public string Name { get; set; }

        public string ApiUrl { get; set; }

        public string WSS_Url { get; set; }

        public int ApiTimeoutMilliseconds { get; set; }

        public int? DelayBetweenCallMilliseonds { get; set; }

        public string FacadeClassName { get; set; }

        public int RateMaxQuantity { get; set; }
        public int RateLimitIntervalSeconds { get; set; }


        public virtual ICollection<ExchangeCustomSettingsDTO> ExchangeCustomSettings { get; set; }
        public virtual ICollection<UserExchangeSecretDTO> UserExchangeSecrets { get; set; }
        public virtual ICollection<LimitArbitrageStrategy4SettingsDTO> LimitStrategy4Settings1 { get; set; }
        public virtual ICollection<LimitArbitrageStrategy4SettingsDTO> LimitStrategy4Settings2 { get; set; }
    }
}
