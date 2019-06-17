using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.DTO.Identity
{
    public class UserStateDTO
    {

        public UserStateDTO(string token, string userName, string email, bool IsAuthenticated, List<string> roles = null)
        {
            Token = token;
            UserName = userName;
            Email = email;
            Roles = roles;
            this.IsAuthenticated = IsAuthenticated;
        }

        public bool IsAuthenticated { get; set; }

        public string Token { get; private set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public List<string> Roles { get; private set; }
    }
}
