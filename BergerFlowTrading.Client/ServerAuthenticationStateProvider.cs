using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BergerFlowTrading.Shared.HttpUnitOfWork;
using Microsoft.AspNetCore.Components;

namespace BergerFlowTrading.Client
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpUnitOfWork uow;

        public ServerAuthenticationStateProvider(HttpUnitOfWork uow)
        {
            this.uow = uow;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine("CheckingAuthState");
            var userInfo = await uow.IdenitytService.GetUserState();

            if (userInfo != null && userInfo.IsAuthenticated)
            { 
                var claims = userInfo.Roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
                claims.Add(new Claim(ClaimTypes.Name, userInfo.UserName));

                var identity = new ClaimsIdentity(claims, "serverauth");

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
