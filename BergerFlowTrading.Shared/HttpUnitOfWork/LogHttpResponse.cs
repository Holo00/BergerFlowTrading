using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BergerFlowTrading.Shared.HttpUnitOfWork
{
    public class LogHttpResponse
    {
        public void LogResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{response.StatusCode.ToString()} {response.ReasonPhrase}");
            }
        }

        public void LogException(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
