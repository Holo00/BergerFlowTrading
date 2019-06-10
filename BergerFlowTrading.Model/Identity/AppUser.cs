using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BergerFlowTrading.Model.Identity
{
    public class AppUser : IdentityUser
    {
        [InverseProperty("User")]
        public virtual ICollection<UserExchangeSecret> UserExchangeSecrets { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<LimitArbitrageStrategy4Settings> LimitStrategy4Settings { get; set; }
    }
}
