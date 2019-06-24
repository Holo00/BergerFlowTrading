using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository.Logs;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Data.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading
{
    public class TradingPlatform: IDisposable
    {
        private readonly StrategySettingsFactory settingsFactory;
        private readonly StrategyFactory strategyFactory;
        private readonly PlatformJobsRepository jobRepo;

        private string userId { get; set; }

        private CancellationTokenSource stoppingToken { get; set; }

        private PlatformJobsDTO platformJob { get; set; }

        private PlatformLogService PlatformLogService { get; set; }

        public Dictionary<string, CancellationTokenSource> strategyTokens { get; private set; }

        private Dictionary<string, SemaphoreSlim> currencySemaphosres;
        private SemaphoreSlim concurrentSemaphore;

        private List<IStrategy> strategies { get; set; }
        public bool Started { get; internal set; }

        public TradingPlatform(StrategySettingsFactory settingsFactory
                                , StrategyFactory strategyFactory
                                , PlatformJobsRepository jobRepo
                                , PlatformLogService PlatformLogService
            )
        {
            this.settingsFactory = settingsFactory;
            this.strategyFactory = strategyFactory;
            this.jobRepo = jobRepo;
            this.PlatformLogService = PlatformLogService;
            this.strategyTokens = new Dictionary<string, CancellationTokenSource>();

            this.userId = userId;


            this.strategies = new List<IStrategy>();
        }

        public async Task StartPlatformJob(CancellationTokenSource stoppingToken, string userId)
        {
            this.stoppingToken = stoppingToken;
            PlatformJobsDTO platformJob = await this.jobRepo.Insert(new PlatformJobsDTO() { StartTime = DateTime.UtcNow }, userId);
            this.PlatformLogService.Log(platformJob.ID, userId, "Starting Platform Job...", eventType.Info);

            while(!stoppingToken.IsCancellationRequested)
            {
                this.PlatformLogService.Log(platformJob.ID, userId, "Running Strategies...", eventType.Info);
                await this.RunStrategies();
                await Task.Delay(60 * 10 * 1000, stoppingToken.Token);
            }
        }

        public async Task StopPlatformJob()
        {
            this.PlatformLogService.Log(platformJob.ID, userId, "Stopping Platform Job...", eventType.Info);
            IEnumerable<IStrategySettingDTO> runningStrats = this.strategies.Select(x => x.strategyInfo);
            await this.StopStrategies(runningStrats, new List<IStrategySettingDTO>());
            this.Dispose();
        }

        public async Task<bool> RunStrategies()
        {
            try
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
                await this.DisposeOfStrategies(stoppedStrats);
                //Dispose of the unused exchanges
                this.DisposeOfExchanges(stoppedStrats);

                return true;
            }
            catch(Exception ex)
            {
                this.PlatformLogService.LogException(platformJob.ID, userId, ex);
                return false;
            }
        }

        private async Task DisposeOfStrategies(IEnumerable<IStrategy> strats)
        {
            foreach(IStrategy s in strats)
            {
                if (s.IsRunning)
                {
                    await s.Stop();
                }

                var token = this.strategyTokens[s.Name];

                if(token != null)
                {
                    token.Cancel();
                }

                s.Dispose();
            }
        }

        private void DisposeOfExchanges(IEnumerable<IStrategy> strats)
        {
            this.strategyFactory.DisposeOfExchanges(strats, this.strategies);
        }


        private async Task StartStrategies(IEnumerable<IStrategySettingDTO> newStrats)
        {
            var newStrategies = newStrats.Where(x => !this.strategies.Select(y => y.strategyInfo).Contains(x));

            List<IStrategy> toStartStrats = new List<IStrategy>();

            List<Task<IStrategy>> tasks = new List<Task<IStrategy>>();

            foreach (IStrategySettingDTO s in newStrategies)
            {
                tasks.Add(strategyFactory.CreateStrategy(s, this.currencySemaphosres, this.concurrentSemaphore));
            }

            List<IStrategy> strats = (await Task.WhenAll(tasks)).ToList();
            toStartStrats.AddRange(strats);

            //Start Strategies
            foreach (IStrategy strat in toStartStrats)
            {
                this.PlatformLogService.Log(platformJob.ID, userId, $"Starting strategy {strat.Name}", eventType.Info);
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
            this.PlatformLogService.Log(platformJob.ID, userId, $"Stopping strategy {runningStrategy.Name}", eventType.Info);
            await runningStrategy.Stop();
            return true;
        }

        public void Dispose()
        {
            this.stoppingToken.Cancel();
            Task.Run(async () => { await this.DisposeOfStrategies(this.strategies); }).Wait();
            this.strategies.Clear();
            this.DisposeOfExchanges(this.strategies);
            GC.SuppressFinalize(this.strategyFactory);
            GC.SuppressFinalize(this.settingsFactory);
            GC.SuppressFinalize(this);
        }
    }
}
