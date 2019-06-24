using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy
{
    public enum StrategyType
    {
        LimitArbitrage4
    }

    public interface IStrategy: IDisposable
    {
        Task Start(CancellationTokenSource token);
        Task Stop();
        bool IsRunning { get; }
        string Name { get; }
        StrategyType Type { get; }
        IStrategySettingDTO strategyInfo { get; }
    }
}
