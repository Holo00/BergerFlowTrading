using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository.Logs;
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
    public class PlatformLogsController : Controller
    {
        private readonly PlatformJobsRepository jobRepo;
        private readonly ILoggingService log;

        public PlatformLogsController(PlatformJobsRepository jobRepo, ILoggingService log)
        {
            this.jobRepo = jobRepo;
            this.log = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetFromDates([FromQuery(Name = "StartDate")] string StartDate, [FromQuery(Name = "EndDate")] string EndDate)
        {
            try
            {
                //string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                //var dtos = await this.jobRepo.GetFromDates(userId, StartDate, EndDate);
                //return Ok(dtos);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }
    }
}
