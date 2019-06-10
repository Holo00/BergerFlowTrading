using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model
{
    public class Exchange : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public string ApiUrl { get; set; }

        public string WSS_Url { get; set; }

        public string FacadeClassName { get; set; }

        public int ApiTimeoutMilliseconds { get; set; }

        public int? DelayBetweenCallMilliseonds { get; set; }

        public int RateMaxQuantity { get; set; }
        public int RateLimitIntervalSeconds { get; set; }


        [InverseProperty("Exchange")]
        public virtual ICollection<ExchangeCustomSettings> ExchangeCustomSettings { get; set; }

        [InverseProperty("Exchange")]
        public virtual ICollection<UserExchangeSecret> UserExchangeSecrets { get; set; }

        [InverseProperty("Exchange_1")]
        public virtual ICollection<LimitArbitrageStrategy4Settings> LimitStrategy4Settings1 { get; set; }

        [InverseProperty("Exchange_2")]
        public virtual ICollection<LimitArbitrageStrategy4Settings> LimitStrategy4Settings2 { get; set; }
    }
}
