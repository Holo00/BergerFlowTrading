using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading
{
    public class TradingPlatform
    {
        private readonly StrategySettingsFactory settingsFactory;

        private readonly ILoggingService logger;

        public Dictionary<string, CancellationTokenSource> strategyTokens { get; private set; }

        private SemaphoreSlim concurrentSemaphore;
        private Dictionary<string, SemaphoreSlim> currencySemaphores { get; set; }

        private List<IStrategy> strategies { get; set; }


        public TradingPlatform(StrategySettingsFactory settingsFactory
                                , ILoggingService logger


            )
        {
            this.settingsFactory = settingsFactory;
            this.strategyTokens = new Dictionary<string, CancellationTokenSource>();

            this.concurrentSemaphore = new SemaphoreSlim(100, 100);
            this.currencySemaphores = new Dictionary<string, SemaphoreSlim>();

            this.strategies = new List<IStrategy>();
        }


        public async Task<bool> RunStrategies()
        {
            List<IStrategySettingDTO> newStraetgies = await this.settingsFactory.LoadStrategies();
            IEnumerable<IStrategySettingDTO> runningStrats = this.strategies.Select(x => x.strategyInfo);

            List<Task<IEnumerable<IStrategySettingDTO>>> stoppingTask = new List<Task<IEnumerable<IStrategySettingDTO>>>();

            //Stop strategies the were removed
            //Removes stopped strategies from the pool
            stoppingTask.Add(this.StopStrategies(runningStrats, newStraetgies));

            //Stop strategies that were updated
            //Removes updated strategies from the pool
            stoppingTask.Add(this.StopUpdateStrategies(runningStrats, newStraetgies));

            List<IStrategySettingDTO> stoppedStratsDTO = (await Task.WhenAll(stoppingTask)).SelectMany(x => x).ToList();
            IEnumerable<IStrategy> stoppedStrats = this.strategies.Where(x => stoppedStratsDTO.Contains(x.strategyInfo));
            this.strategies.RemoveAll(x => stoppedStratsDTO.Contains(x.strategyInfo));

            //Start strategies that were added
            //Add strategies to the pool
            await this.StartStrategies(newStraetgies);

            //dispose of unused strategies
            //Dispose of the unused exchanges

            return true;
        }

        private void DisposeOfStrategies(IEnumerable<IStrategy> strats)
        {

        }

        private void DisposeOfStrategies(IEnumerable<IStrategySettingDTO> strats)
        {

        }


        private async Task StartStrategies(IEnumerable<IStrategySettingDTO> newStrats)
        {
            var newStrategies = newStrats.Where(x => !this.strategies.Select(y => y.strategyInfo).Contains(x));

            List<IStrategy> toStartStrats = new List<IStrategy>();

            foreach (IStrategySettingDTO s in newStrategies)
            {
                IStrategy strat = StrategyFactory.CreateStrategy(s, this.GetCurrencySemaphore, this.concurrentSemaphore, this.logger);

                if(strat != null)
                {
                    toStartStrats.Add(strat);
                }
            }

            //Start Strategies
            foreach (IStrategy strat in toStartStrats)
            {
                CancellationTokenSource token = new CancellationTokenSource();
                this.strategyTokens.Add(strat.Name, token);
                await strat.Start(token);
            }

            this.strategies.AddRange(toStartStrats);
        }





        private async Task<IEnumerable<IStrategySettingDTO>> StopStrategies(IEnumerable<IStrategySettingDTO> runningStrats, IEnumerable<IStrategySettingDTO> newStrats)
        {
            List<IStrategySettingDTO> toremoveStrats = runningStrats.Where(x => !newStrats.Select(y => y.StrategyName).Contains(x.StrategyName)).ToList();

            await Task.WhenAll(toremoveStrats.Select(x => this.StopStrategy(x)).ToList());
            return toremoveStrats;
        }

        private async Task<IEnumerable<IStrategySettingDTO>> StopUpdateStrategies(IEnumerable<IStrategySettingDTO> runningStrats, IEnumerable<IStrategySettingDTO> newStrats)
        {
            List<IStrategySettingDTO> toremoveStrats = runningStrats.Where(x => newStrats.Select(y => y.StrategyName).Contains(x.StrategyName)).ToList();
            
            foreach(IStrategySettingDTO strat in newStrats)
            {
                IStrategySettingDTO toRemove = toremoveStrats.FirstOrDefault(x => x.StrategyName == strat.StrategyName && x.UpdatedTimeStamp == x.UpdatedTimeStamp);

                if(toRemove != null)
                {
                    toremoveStrats.Remove(toRemove);
                }
            }

            await Task.WhenAll(toremoveStrats.Select(x => this.StopStrategy(x)).ToList());
            return toremoveStrats;
        }

        private async Task<bool> StopStrategy(IStrategySettingDTO strat)
        {
            IStrategy runningStrategy = this.strategies.FirstOrDefault(x => x.Name == strat.StrategyName);
            await runningStrategy.Stop();
            return true;
        }

        public SemaphoreSlim GetCurrencySemaphore(string currency)
        {
            var sem = currencySemaphores[currency];

            if (sem == null)
            {
                sem = new SemaphoreSlim(1, 1);
                currencySemaphores[currency] = sem;
            }

            return sem;
        }
    }
}
