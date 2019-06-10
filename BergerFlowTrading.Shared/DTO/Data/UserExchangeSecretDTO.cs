using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class UserExchangeSecretDTO: UserDataDTO
    {
        public int Exchange_ID { get; set; }
        public ExchangeDTO Exchange { get; set; }
        public string Api_ID { get; set; }
        public string Api_Secret { get; set; }
    }
}
