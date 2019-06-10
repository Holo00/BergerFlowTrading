using BergerFlowTrading.Shared.DTO.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.Shared.HttpUnitOfWork.Repository
{
    public class HttpLimitArbitrageStrategy4SettingsRepository : HttpBaseRepository<LimitArbitrageStrategy4SettingsDTO>
    {
        public HttpLimitArbitrageStrategy4SettingsRepository(HttpClient http, LogHttpResponse log) : base(http, log)
        {
            this.Path = "LimitArbitrage4Settings";
        }

        public async Task<bool> ChangeActive(int id)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            var response = await http.PutAsync($"api/v1/{Path}/ChangeActive", stringContent);

            this.log.LogResponse(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeBalanceOn(int id)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            var response = await http.PutAsync($"api/v1/{Path}/ChangeBalanceOn", stringContent);

            this.log.LogResponse(response);
            return response.IsSuccessStatusCode;
        }
    }
}
