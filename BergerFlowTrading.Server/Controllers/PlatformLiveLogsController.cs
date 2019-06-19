using BergerFlowTrading.BusinessTier.BackgroundService;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository.Logs;
using BergerFlowTrading.Shared.DTO.Data.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BergerFlowTrading.Server.Controllers
{
    [Authorize(Roles = "BasicPlan")]
    [Route("api/v1/[controller]/[action]")]
    public class PlatformLiveLogsController : Controller
    {
        private readonly PlatformJobsRepository jobRepo;
        private readonly PlatformLogsRepository jobLogsRepo;
        private readonly StrategyRunsRepository runsRepo;
        private readonly StrategyLogsRepository runsLogRepo;
        private readonly ExchangeLogsRepository exchangeLogsRepo;

        private readonly TradingJobServiceFactory jobsFactory;

        private readonly ILoggingService log;

        public PlatformLiveLogsController(  PlatformJobsRepository jobRepo
                                            , PlatformLogsRepository jobLogsRepo
                                            , StrategyRunsRepository runsRepo
                                            , StrategyLogsRepository runsLogRepo
                                            , ExchangeLogsRepository exchangeLogsRepo
                                            , TradingJobServiceFactory jobsFactory
                                            , ILoggingService log)
        {
            this.jobRepo = jobRepo;
            this.jobLogsRepo = jobLogsRepo;
            this.runsRepo = runsRepo;
            this.runsLogRepo = runsLogRepo;
            this.exchangeLogsRepo = exchangeLogsRepo;
            this.jobsFactory = jobsFactory;
            this.log = log;
        }


        [HttpGet]
        public async Task<IActionResult> GetCurrentPlatformJob()
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                bool isRunning = this.jobsFactory.IsPlatformJobRunning(userId);

                PlatformJobsDTO job = null;

                if (isRunning)
                {
                    job = await jobRepo.GetLast(userId);
                    job.IsRunning = true;
                }

                return Ok(job);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> StartPlatformJob()
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                bool isRunning = this.jobsFactory.IsPlatformJobRunning(userId);

                PlatformJobsDTO job = null;

                if (!isRunning)
                {
                    await this.jobsFactory.StartPlatform(userId);
                    job = await jobRepo.GetLast(userId);
                    job.IsRunning = true;
                }

                return Ok(job);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> StopPlatformJob()
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                bool isRunning = this.jobsFactory.IsPlatformJobRunning(userId);

                PlatformJobsDTO job = null;

                if (isRunning)
                {
                    await this.jobsFactory.StopPlatform(userId);
                    job = await jobRepo.GetLast(userId);
                    job.IsRunning = false;
                }

                return Ok(job);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPlatformLogs([FromQuery(Name = "fromLast")]int? fromLast = null)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                bool isRunning = this.jobsFactory.IsPlatformJobRunning(userId);

                List<PlatformLogsDTO> logs = new List<PlatformLogsDTO>();

                if (isRunning)
                {
                    var job = await jobRepo.GetLast(userId);
                    logs = await this.jobLogsRepo.GetByJobID(job.ID.Value, userId, fromLast);
                }

                return Ok(logs);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStrategyLogs([FromQuery(Name = "strategyRunID")]int runID, [FromQuery(Name = "fromLast")]int? fromLast = null)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                bool isRunning = this.jobsFactory.IsPlatformJobRunning(userId);

                List<StrategyLogsDTO> logs = new List<StrategyLogsDTO>();

                if (isRunning)
                {
                    logs = await this.runsLogRepo.GetByRunID(runID, userId, fromLast);
                }

                return Ok(logs);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExchangetLogs(ExchangeLogType type, [FromQuery(Name = "filter")]string filter = null, [FromQuery(Name = "fromLast")]int? fromLast = null)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                bool isRunning = this.jobsFactory.IsPlatformJobRunning(userId);

                List<ExchangeLogsDTO> logs = new List<ExchangeLogsDTO>();

                if (isRunning)
                {
                    var job = await jobRepo.GetLast(userId);
                    logs = await this.exchangeLogsRepo.GetByExchangeID(job.ID.Value, userId, type, filter, fromLast);
                }

                return Ok(logs);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }


    }
}
