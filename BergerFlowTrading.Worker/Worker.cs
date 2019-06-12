using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BergerFlowTrading.BusinessTier.Services;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BergerFlowTrading.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TradingPlatform platform;

        public Worker(ILogger<Worker> logger
                    , TradingPlatform platform
            )
        {
            this._logger = logger;
            this.platform = platform;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await platform.RunStrategies();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}
