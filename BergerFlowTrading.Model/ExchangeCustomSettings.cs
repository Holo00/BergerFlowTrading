using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model
{
    public class ExchangeCustomSettings : BaseModel
    {
        [Required]
        public int Exchange_ID { get; set; }

        [ForeignKey("Exchange_ID")]
        public virtual Exchange Exchange { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
