using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public class ExchangeCustomSettingsDTO: DataDTOBase
    {
        public int Exchange_ID { get; set; }

        public virtual ExchangeDTO Exchange { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
