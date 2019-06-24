using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading.Strategy
{
    public class Limit4DealInProgress
    {
        public Limit4CurrentMode Limit4CurrentMode { get; set; }
    }

    public enum Limit4CurrentMode
    {
        None,
        ManageBalance,
        Arbitrage
    }
}
