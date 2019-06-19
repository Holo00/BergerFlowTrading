using BergerFlowTrading.Client.Extensions;
using BergerFlowTrading.Client.Services;
using BergerFlowTrading.Shared.HttpUnitOfWork;
using BergerFlowTrading.Shared.HttpUnitOfWork.Identity;
using BergerFlowTrading.Shared.HttpUnitOfWork.Repository;
using BergerFlowTrading.Shared.Storage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BergerFlowTrading.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, NullLogger>();

            services.AddEnvironmentConfiguration<Startup>(() =>
                new EnvironmentChooser("Development")
                    .Add("localhost", "Development")
                    .Add("", "Production", false));


            services.AddSingleton<LocalStorageService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<LogHttpResponse>();
            services.AddSingleton<IdentityService>();
            services.AddSingleton<HttpExchangeRepository>();
            services.AddSingleton<HttpUserExchangeSecretRepository>();
            services.AddSingleton<HttpLimitArbitrageStrategy4SettingsRepository>();
            services.AddSingleton<HttpPlatformLiveLogsRepository>();
            services.AddSingleton<HttpUnitOfWork>();

            //services.AddTransient<HubConnectionBuilder>();

            // Add auth services
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
