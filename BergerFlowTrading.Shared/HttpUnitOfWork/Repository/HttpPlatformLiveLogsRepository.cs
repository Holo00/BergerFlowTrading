using BergerFlowTrading.Shared.DTO.Data.Logs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.Shared.HttpUnitOfWork.Repository
{
    public class HttpPlatformLiveLogsRepository
    {
        protected readonly HttpClient http;
        protected readonly LogHttpResponse log;

        protected string Path { get; set; }

        public HttpPlatformLiveLogsRepository(HttpClient http, LogHttpResponse log)
        {
            this.http = http;
            this.log = log;
            this.Path = "PlatformLiveLogs";
        }

        public async Task<PlatformJobsDTO> GetCurrentPlatformJob()
        {
            var response = await http.GetAsync($"api/v1/{Path}/GetCurrentPlatformJob");

            this.log.LogResponse(response);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<PlatformJobsDTO>(data);
                return returnDto;
            }
            return null;
        }

        public async Task<PlatformJobsDTO> StartPlatformJob()
        {
            var response = await http.GetAsync($"api/v1/{Path}/StartPlatformJob");

            this.log.LogResponse(response);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<PlatformJobsDTO>(data);
                return returnDto;
            }
            return null;
        }

        public async Task<PlatformJobsDTO> StoptPlatformJob()
        {
            var response = await http.GetAsync($"api/v1/{Path}/StopPlatformJob");

            this.log.LogResponse(response);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<PlatformJobsDTO>(data);
                return returnDto;
            }
            return null;
        }

        public async Task<List<PlatformLogsDTO>> GetAllCurrentPlatformLogs(int? fromLast = null)
        {
            string queryString = $"?";

            if(fromLast.HasValue)
            {
                queryString += $"fromLast={fromLast.Value}";
            }

            queryString = queryString != $"?" ? queryString : "";


            var response = await http.GetAsync($"api/v1/{Path}/GetAllPlatformLogs{queryString}");

            this.log.LogResponse(response);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<List<PlatformLogsDTO>>(data);
                return returnDto;
            }
            return new List<PlatformLogsDTO>();
        }

        public async Task<List<StrategyLogsDTO>> GetAllCurrentStrategyLogs(string strategyRunID, int? fromLast = null)
        {
            string queryString = $"?strategyRunID={strategyRunID}";

            if (fromLast.HasValue)
            {
                queryString += $"&fromLast={fromLast.Value}";
            }

            var response = await http.GetAsync($"api/v1/{Path}/GetAllStrategyLogs{queryString}");

            this.log.LogResponse(response);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<List<StrategyLogsDTO>>(data);
                return returnDto;
            }
            return new List<StrategyLogsDTO>();
        }

        public async Task<List<ExchangeLogsDTO>> GetAllCurrentExchangeLogs(ExchangeLogType type, string filter = null, int? fromLast = null)
        {
            string queryString = $"?type={type}";

            if (filter != null)
            {
                queryString += $"&filter={filter}";
            }

            if (fromLast.HasValue)
            {
                queryString += $"&fromLast={fromLast.Value}";
            }

            var response = await http.GetAsync($"api/v1/{Path}/GetAllStrategyLogs{queryString}");

            this.log.LogResponse(response);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<List<ExchangeLogsDTO>>(data);
                return returnDto;
            }
            return new List<ExchangeLogsDTO>();
        }
    }
}
