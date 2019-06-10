using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Identity
{
    public enum UserStateMode
    {
        Init,
        SignedIn,
        SignInInProgress,
        SignUpInProgress,
        SignedOut
    }
}
