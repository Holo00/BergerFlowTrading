using BergerFlowTrading.BusinessTier.Services.AutomatedTrading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.BackgroundService
{
    public class TradingJobServiceFactory
    {
        private Dictionary<string, TradingPlatform> runningPlatforms { get; set; }
        private IServiceProvider provider { get; set; }

        public TradingJobServiceFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public bool IsPlatformJobRunning(string userId)
        {
            return runningPlatforms[userId] != null && runningPlatforms[userId].Started;
        }

        public async Task<bool> StartPlatform(string userId)
        {
            if(!IsPlatformJobRunning(userId))
            {
                TradingPlatform platform = (TradingPlatform)provider.GetService(typeof(TradingPlatform));
                runningPlatforms[userId] = platform;
                CancellationTokenSource token = new CancellationTokenSource();
                await platform.StartPlatformJob(token.Token, userId);
            }

            return IsPlatformJobRunning(userId);
        }

        public async Task<bool> StopPlatform(string userId)
        {
            if (IsPlatformJobRunning(userId))
            {
                await runningPlatforms[userId].StopPlatformJob();
                this.runningPlatforms.Remove(userId);
            }

            return IsPlatformJobRunning(userId);
        }
    }
}
