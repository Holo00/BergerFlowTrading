using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.Shared.HttpUnitOfWork.Repository
{
    public abstract class HttpBaseRepository<DTO> where DTO : class
    {
        protected readonly HttpClient http;
        protected readonly LogHttpResponse log;

        protected virtual string Path { get; set; }

        public HttpBaseRepository(HttpClient http, LogHttpResponse log)
        {
            this.http = http;
            this.log = log;
        }

        public virtual async Task<DTO> GetById(int id)
        {
            Console.WriteLine($"api/v1/{Path}/GetById/{id}");
            var response = await http.GetAsync($"api/v1/{Path}/GetById/{id}");

            this.log.LogResponse(response);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<DTO>(data);
                return returnDto;
            }
            return null;
        }

        public virtual async Task<List<DTO>> GetAll()
        {
            Console.WriteLine($"api/v1/{Path}/GetAll");
            Console.WriteLine(http.DefaultRequestHeaders.Authorization.ToString());
            var response = await http.GetAsync($"api/v1/{Path}/GetAll");

            this.log.LogResponse(response);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var returnDto = JsonConvert.DeserializeObject<IEnumerable<DTO>>(data);
                return returnDto.ToList();
            }
            return null;
        }

        public virtual async Task<bool> Create(DTO dto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"api/v1/{Path}/Create", stringContent);

            this.log.LogResponse(response);
            return response.IsSuccessStatusCode;
        }

        public virtual async Task<bool> Update(DTO dto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await http.PutAsync($"api/v1/{Path}/Update", stringContent);

            this.log.LogResponse(response);
            return response.IsSuccessStatusCode;
        }

        public virtual async Task<bool> Delete(int id)
        {
            var response = await http.DeleteAsync($"api/v1/{Path}/Delete/{id}");

            this.log.LogResponse(response);
            return response.IsSuccessStatusCode;
        }
    }
}
