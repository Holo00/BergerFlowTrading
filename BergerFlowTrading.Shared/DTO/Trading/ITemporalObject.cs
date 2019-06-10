using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public interface ITemporalObject
    {
        /// <summary>
        /// Last Time  the object was Updated (UTC)
        /// </summary>
        DateTime? LastUpdated { get; set; }
    }
}
