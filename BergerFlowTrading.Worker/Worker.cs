using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BergerFlowTrading.BusinessTier.Services;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BergerFlowTrading.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TradingPlatform platform;

        public Worker(ILogger<Worker> logger
                    //, TradingPlatform platform
            )
        {
            this._logger = logger;
            this.platform = platform;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var hub = new HubConnectionBuilder()
            .WithUrl($"https://localhost:44395/TradingPlatformHub"
            //,options =>
            //{
            //    options.AccessTokenProvider = () => Task.FromResult(token);
            //}
            )
            .Build();

            //await platform.StartPlatformJob(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                //await platform.RunStrategies();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(2000, stoppingToken);
               // await Task.Delay(3600000, stoppingToken);
            }

            //await platform.StopPlatformJob();
        }
    }
}
