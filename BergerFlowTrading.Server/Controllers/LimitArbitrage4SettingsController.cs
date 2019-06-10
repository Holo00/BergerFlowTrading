using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository;
using BergerFlowTrading.Shared.DTO.Data;
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
    public class LimitArbitrage4SettingsController : Controller
    {
        private readonly LimitStrategy4SettingsRepository repo;
        private readonly ILoggingService log;

        public LimitArbitrage4SettingsController(LimitStrategy4SettingsRepository repo, ILoggingService log)
        {
            this.repo = repo;
            this.log = log;
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                LimitArbitrageStrategy4SettingsDTO dto = await this.repo.GetById(id, userId);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var dtos = await this.repo.GetAll(userId);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]LimitArbitrageStrategy4SettingsDTO dto)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                await this.repo.Insert(dto, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]LimitArbitrageStrategy4SettingsDTO dto)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                await this.repo.Update(dto, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                await this.repo.Delete(id, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangeActive([FromBody]int id)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var dto = await this.repo.GetById(id, userId);
                dto.Active = !dto.Active;
                await this.repo.Update(dto, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }


        [HttpPut]
        public async Task<IActionResult> ChangeBalanceOn([FromBody]int id)
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var dto = await this.repo.GetById(id, userId);
                dto.ManagementBalanceON = !dto.ManagementBalanceON;
                await this.repo.Update(dto, userId);
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
