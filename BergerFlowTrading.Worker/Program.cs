using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BergerFlowTrading.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    if(builderContext.HostingEnvironment.IsDevelopment())
                    {
                        config.AddUserSecrets<Program>();
                    }
                })
                .ConfigureServices(services =>
                {
                    services.AddScoped<ILoggingService, LoggingService>();
                    services.AddScoped<LimitStrategy4SettingsRepository>();
                    services.AddScoped<UserExchangeSecretRepository>();
                    services.AddScoped<TradingPlatform>();
                    services.AddHostedService<Worker>();
                });
    }
}
