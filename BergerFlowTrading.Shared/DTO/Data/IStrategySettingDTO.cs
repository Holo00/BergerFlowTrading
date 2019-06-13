using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Data
{
    public interface IStrategySettingDTO
    {
        string StrategyName { get; set; }

        StrategyType StrategyType { get; set; }

        DateTime UpdatedTimeStamp { get; set; }
    }
}
