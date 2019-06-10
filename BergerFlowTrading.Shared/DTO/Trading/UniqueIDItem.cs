using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public abstract class UniqueIDItem
    {
        public Guid UniqueID { get; private set; }

        public UniqueIDItem()
        {
            this.UniqueID = new Guid();
        }
    }
}
