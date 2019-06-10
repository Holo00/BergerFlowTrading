using BergerFlowTrading.Shared.DTO.Identity;
using BergerFlowTrading.Shared.Storage;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.Shared.HttpUnitOfWork.Identity
{
    public class IdentityService
    {
        private readonly IHttpContextAccessor contextAccess;
        private readonly HttpClient http;
        private readonly LogHttpResponse log;
        private readonly LocalStorageService storage;

        private HttpContext currentContext { get; set; }

        public IdentityService(IHttpContextAccessor contextAccess, HttpClient http, LogHttpResponse log, LocalStorageService storage)
        {
            this.contextAccess = contextAccess;
            this.http = http;
            this.log = log;
            this.storage = storage;
        }

        public async Task<bool> Register(RegisterDTO dto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("api/v1/User/Register", stringContent);

            this.log.LogResponse(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<UserStateDTO> GetUserState()
        {
            try
            {
                string s = await storage.GetLocalStorageValue<string>("JWT");

                if(!String.IsNullOrEmpty(s))
                {
                    var user = JsonConvert.DeserializeObject<UserStateDTO>(s);
                    this.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    return user;
                }

                this.http.DefaultRequestHeaders.Authorization = null;
                return null;
            }
            catch (Exception ex)
            {
                log.LogException(ex);
                return null;
            }
        }

        public async Task<bool> SetUserState(UserStateDTO dto)
        {
            try
            {
                string s = JsonConvert.SerializeObject(dto);
                await storage.SetLocalStorageValue<string>("JWT", s);
            }
            catch(Exception ex)
            {
                log.LogException(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> SignOut()
        {
            await storage.RemoveLocalStorageValue("JWT");
            return true;
        }


        public async Task<UserStateDTO> Login(LoginDTO dto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("api/v1/User/Login", stringContent);

            this.log.LogResponse(response);

            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<UserStateDTO>(data);
                await this.SetUserState(returnDto);
                await this.GetUserState();
                return returnDto;
            }

            return null;
        }
    }
}
