using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BergerFlowTrading.Shared.HttpUnitOfWork.Repository
{
    public class HttpExchangeRepository: HttpBaseRepository<ExchangeDTO>
    {
        public HttpExchangeRepository(HttpClient http, LogHttpResponse log) : base(http, log)
        {
            this.Path = "Exchange";
        }
    }
}
