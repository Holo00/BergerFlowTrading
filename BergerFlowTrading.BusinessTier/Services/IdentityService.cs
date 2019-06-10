using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model.Identity;
using BergerFlowTrading.Shared.DTO.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services
{
    public class IdentityService
    {
        private ApplicationDbContext ctxt;
        private IMapper mapper;

        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        private IConfiguration _configuration;

        public IdentityService(ApplicationDbContext ctxt,
                                IMapper mapper,
                                UserManager<AppUser> u_Manager,
                                SignInManager<AppUser> signInManager,
                                IConfiguration configuration
                                )
        {
            this.ctxt = ctxt;
            this.mapper = mapper;
            this._userManager = u_Manager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        public IQueryable<AppUser> Users
        {
            get
            {
                return _userManager.Users;
            }
        }

        public async Task<IdentityResult> RegisterNewUser(RegisterDTO dto)
        {
            var userIdentity = this.mapper.Map<AppUser>(dto);
            return await _userManager.CreateAsync(userIdentity, dto.Password);
        }

        public async Task<SignInResult> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
            }

            return await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        }

        public async Task<string> GenerateJwtToken(string email, AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            claims.AddRange(await this.GetRoles(user));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_Auth:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT_Auth:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JWT_Auth:JwtIssuer"],
                _configuration["JWT_Auth:JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<Claim>> GetRoles(AppUser user)
        {
            var roles = await this._userManager.GetRolesAsync(user);
            var claims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            return claims;
        }

        public string GetUserID(string token)
        {
            try
            {
                ClaimsPrincipal principal = this.Authorize(token);

                if (principal != null)
                {
                    var id = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                    return id.Value;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<string> GetRoles(string token)
        {
            try
            {
                List<string> roles = new List<string>();
                ClaimsPrincipal principal = this.Authorize(token);

                if (principal != null)
                {
                    return principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
                }

                return roles;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Authorize(string token, string policyName)
        {
            try
            {
                ClaimsPrincipal principal = this.Authorize(token);

                if (principal != null)
                {
                    if (principal.HasClaim(ClaimTypes.Role, policyName))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private ClaimsPrincipal Authorize(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_Auth:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            SecurityToken validatedToken;
            var validator = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidIssuer = _configuration["JWT_Auth:JwtIssuer"];
            validationParameters.ValidAudience = _configuration["JWT_Auth:JwtAudience"];
            validationParameters.IssuerSigningKey = key;
            validationParameters.ValidateIssuerSigningKey = true;
            validationParameters.ValidateAudience = true;

            if (validator.CanReadToken(token))
            {
                ClaimsPrincipal principal;

                principal = validator.ValidateToken(token, validationParameters, out validatedToken);

                return principal;
            }
            else
            {
                return null;
            }
        }
    }
}
