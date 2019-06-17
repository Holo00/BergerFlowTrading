using System;
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

            var identity = userInfo != null && userInfo.IsAuthenticated
                ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userInfo.UserName) }, "serverauth")
                : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
