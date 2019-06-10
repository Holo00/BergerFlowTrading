using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.Shared.Http
{
    public class HttpRequestHelper
    {
        private HttpClient client;

        public HttpRequestHelper(int timeout = 10000, AuthenticationHeaderValue auth = null)
        {
            client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);

            client.DefaultRequestHeaders.ConnectionClose = false;

            if (auth != null)
            {
                client.DefaultRequestHeaders.Authorization = auth;

            }
        }

        public async Task<HttpResponseMessage> SendRequestAsync(string domain, string url, string verb, object body,
            string contentType = "application/json", bool keepAlive = true,
            List<(string key, string value)> customHeaders = null, AuthenticationHeaderValue auth = null)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(GetMethod(verb), domain + url);
                message.Method = GetMethod(verb);

                if (customHeaders != null && customHeaders.Any())
                {
                    customHeaders.ForEach(x =>
                    {
                        message.Headers.Add(x.key, x.value);
                    });
                }

                if (body != null)
                {
                    if (contentType == "application/x-www-form-urlencoded-2")
                    {
                        message.Content = new ByteArrayContent(Encoding.UTF8.GetBytes((string)body));
                        contentType = "application/x-www-form-urlencoded";
                        message.Content.Headers.ContentLength = ((string)body).Length;
                    }
                    else if (contentType == "application/x-www-form-urlencoded")
                    {
                        List<KeyValuePair<string, string>> content = (List<KeyValuePair<string, string>>)body;
                        message.Content = new FormUrlEncodedContent(content);
                    }
                    else if (body.GetType() == typeof(JObject))
                    {
                        message.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(((JObject)body).ToString()));
                    }
                    else
                    {
                        message.Content = new ByteArrayContent(Encoding.UTF8.GetBytes((string)body));
                    }

                    message.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }

                if (auth != null)
                {
                    message.Headers.Authorization = auth;
                }

                return await client.SendAsync(message);
            }
            catch
            {
                throw;
            }
        }


        public async Task<HttpResponseMessage> SendRequestAsync(string domain, string url, string verb, FormUrlEncodedContent content,
            string contentType = "application/json", bool keepAlive = true, List<(string key, string value)> headers = null)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(GetMethod(verb), domain + url);
                message.Method = GetMethod(verb);


                if (headers != null && headers.Any())
                {
                    headers.ForEach(x =>
                    {
                        message.Headers.Add(x.key, x.value);
                    });
                }

                message.Content = content;
                message.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return await client.SendAsync(message);
            }
            catch
            {
                throw;
            }
        }


        public static HttpMethod GetMethod(string verb)
        {
            switch (verb)
            {
                case "GET":
                    return HttpMethod.Get;
                case "POST":
                    return HttpMethod.Post;
                case "PUT":
                    return HttpMethod.Put;
                case "DELETE":
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Get;
            }
        }
    }
}
