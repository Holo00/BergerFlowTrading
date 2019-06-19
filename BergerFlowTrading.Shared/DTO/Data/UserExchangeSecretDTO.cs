using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class UserExchangeSecretDTO: UserDataDTO
    {
        [Required]
        public int Exchange_ID { get; set; }
        public ExchangeDTO Exchange { get; set; }
        [Required]
        public string Api_ID { get; set; }
        [Required]
        public string Api_Secret { get; set; }
    }
}
