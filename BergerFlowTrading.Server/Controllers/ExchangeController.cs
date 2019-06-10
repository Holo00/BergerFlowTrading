using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository;
using BergerFlowTrading.Shared.DTO.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BergerFlowTrading.Server.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    public class ExchangeController : Controller
    { 
        private readonly ExchangeRepository repo;
        private readonly ILoggingService log;

        public ExchangeController(ExchangeRepository repo, ILoggingService log)
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
                ExchangeDTO dto = await this.repo.GetById(id);
                return Ok(dto);
            }
            catch(Exception ex)
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
                var dtos = await this.repo.GetAll();
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ExchangeDTO dto)
        {
            try
            {
                await this.repo.Insert(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ExchangeDTO dto)
        {
            try
            {
                await this.repo.Update(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Log(ex);
                return BadRequest();
            }
        }

        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.repo.Delete(id);
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
