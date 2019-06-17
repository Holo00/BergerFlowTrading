using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BergerFlowTrading.BusinessTier.Services;
using BergerFlowTrading.Model.Identity;
using BergerFlowTrading.Shared.DTO.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BergerFlowTrading.Server.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IdentityService idService;

        public UserController(IdentityService idService)
        {
            this.idService = idService;
        }

        // POST api/user/register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await idService.RegisterNewUser(dto);

                if (result.Succeeded)
                {
                    return Ok();
                }
                return Ok(string.Join(",", result.Errors?.Select(error => error.Description)));
            }
            string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage ?? "Bad Request");
        }


        // POST api/user/login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await idService.Login(dto);

                if (result.Succeeded)
                {
                    var appUser = idService.Users.SingleOrDefault(r => r.Email == dto.Email);
                    var token = await idService.GenerateJwtToken(dto.Email, appUser);
                    var rootData = new UserStateDTO(token, appUser.UserName, appUser.Email, true, idService.GetRoles(token));

                    return Ok(rootData);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized, "Bad Credentials");
            }
            string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage ?? "Bad Request");
        }
    }
}
