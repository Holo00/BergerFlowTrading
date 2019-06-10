using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BergerFlowTrading.Shared.Http
{
    public class HttpResponseResult
    {
        public HttpStatusCode Code { get; set; }
        public string body { get; set; }
        public DateTimeOffset? time { get; set; }
        public HttpResponseMessage response { get; set; }
    }
}
