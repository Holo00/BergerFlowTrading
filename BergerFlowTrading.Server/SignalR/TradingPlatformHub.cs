using BergerFlowTrading.BusinessTier.BackgroundService;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BergerFlowTrading.Server.SignalR
{
    //[Authorize(Roles = "Admin")]
    public class TradingPlatformHub : Hub
    {
        private TradingJobServiceFactory facto { get; set; }

        public TradingPlatformHub(TradingJobServiceFactory facto)
        {
            this.facto = facto;
        }

        public async Task IsPlatformJobRunning()
        {
            await Task.Delay(10000);
            //bool result = this.facto.IsPlatformJobRunning(userId);
            //return Clients.Caller.SendAsync("IsPlatformJobRunning_Result", result);
        }
    }
}
