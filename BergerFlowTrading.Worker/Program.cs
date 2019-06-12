using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.DataTier.Repository;
using BergerFlowTrading.Model.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BergerFlowTrading.Worker
{
    public class Program
    {
        private IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder =  Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    if (builderContext.HostingEnvironment.IsDevelopment())
                    {
                        config.AddUserSecrets<Program>();
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Auto Mapper Configurations
                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new DataMapper());
                    });

                    IMapper mapper = mappingConfig.CreateMapper();
                    services.AddSingleton(mapper);

                    services.AddDbContext<ApplicationDbContext>(options =>
                      options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"),
                      b => b.MigrationsAssembly("BergerFlowTrading.DataTier")), 
                      ServiceLifetime.Singleton);

                    services.AddSingleton<ILoggingService, LoggingService>();
                    services.AddSingleton<LimitStrategy4SettingsRepository>();
                    services.AddSingleton<UserExchangeSecretRepository>();
                    services.AddSingleton<ExchangeFactory>();
                    services.AddSingleton<StrategyFactory>();
                    services.AddSingleton<StrategySettingsFactory>();
                    services.AddSingleton<TradingPlatform>();
                    services.AddHostedService<Worker>();

                });

            return builder;
        }
    }
}
