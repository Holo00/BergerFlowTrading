using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
using BergerFlowTrading.Shared.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public abstract class SpotExchangeApiBase : ISpotExchangeApi
    {
        protected List<(DateTime time, string name)> requests;
        protected HttpRequestHelper http { get; set; }
        protected ExchangeDTO settings { get; set; }
        protected ILoggingService log { get; set; }
        protected UserExchangeSecretDTO secrets { get; set; }


        protected DateTime? lastCall { get; set; }
        protected int? DelayBetCall { get; set; }


        protected TimeSpan rateLimitInterval { get; set; }
        protected int RateQuantityMax { get; set; }


        public SpotExchangeApiBase(ExchangeDTO exchangeSettings, ILoggingService logger, UserExchangeSecretDTO secrets)
        {
            this.requests = new List<(DateTime time, string name)>();
            this.http = new HttpRequestHelper(exchangeSettings.ApiTimeoutMilliseconds);
            this.settings = exchangeSettings;
            this.log = logger;

            this.DelayBetCall = exchangeSettings.DelayBetweenCallMilliseonds;
            this.RateQuantityMax = exchangeSettings.RateMaxQuantity;
            this.rateLimitInterval = new TimeSpan(0, 0, exchangeSettings.RateLimitIntervalSeconds);
            this.secrets = secrets;
        }

        protected async virtual Task<HttpResponseResult> ExecuteRequest(string endpoint, string queryString, string verb, object body = null, string contentType = "application/json", List<(string, string)> headers = null, bool logCalls = false, AuthenticationHeaderValue auth = null)
        {
            string msg = "";

            //Delay the call to not spam
            if (lastCall != null && this.DelayBetCall != null && lastCall.Value.AddMilliseconds(this.DelayBetCall.Value) < DateTime.UtcNow)
            {
                await Task.Delay(this.DelayBetCall.Value);
                //log.LogInformation("Delayed!!!!!!!!!!!!!!1");
            }

            lastCall = DateTime.UtcNow;

            string fullUrl = $"{endpoint}";

            if (!string.IsNullOrEmpty(queryString))
            {
                fullUrl += $"?{queryString}";
            }

            HttpResponseMessage response = null;
            HttpResponseResult result = new HttpResponseResult();

            if (logCalls)
            {
                log.Log($"{verb} {fullUrl} Begin Request!");
            }

            DateTime dt = DateTime.Now;

            try
            {
                response = await http.SendRequestAsync(settings.ApiUrl, fullUrl, verb, body, contentType, true, headers, auth);
                result.body = await response.Content.ReadAsStringAsync();
                result.Code = response.StatusCode;
                result.time = response.Headers.Date;
                result.response = response;

                if (response.IsSuccessStatusCode)
                {
                    if (logCalls)
                    {
                        TimeSpan span = DateTime.Now - dt;
                        log.Log($"{verb} {fullUrl} Received! Time to complete http request: {span.TotalMilliseconds.ToString("0")} ms");
                    }
                }
                else if (response.StatusCode != HttpStatusCode.NotFound)
                {
                    TimeSpan span = DateTime.Now - dt;
                    //log.LogError($"Code: {response.StatusCode} {verb} Failed : {body} {Environment.NewLine}Time to complete http request: {span.TotalMilliseconds.ToString("0")} ms{Environment.NewLine}{result.body}");

                    if (response.StatusCode == 0)
                    {
                        result.body = $"Code: {response.StatusCode} {verb} Failed : {body} {Environment.NewLine}Time to complete http request: {span.TotalMilliseconds.ToString("0")} ms{Environment.NewLine}{result.body}";
                    }

                    if (result.body.Contains("429"))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                TimeSpan span = DateTime.Now - dt;
                msg += $"{verb} {settings.ApiUrl} {fullUrl} Failed : {body} {Environment.NewLine}Time to complete http request: {span.TotalMilliseconds.ToString("0")} ms{Environment.NewLine}{result.body}";

                if (ex.Message.Contains("429"))
                {

                }

                if (ex.Message.Contains("A task was canceled.") || ex.Message.Contains("The operation was canceled."))
                {
                    //Reset because of timeout
                    result.Code = HttpStatusCode.RequestTimeout;
                    result.body = "A task was canceled.";
                }
                else
                {
                    result.body = msg + Environment.NewLine + ex.ToString();
                }
            }

            return result;
        }


        protected abstract Task<HttpResponseResult> _GetBalance(string currency, bool force = false);
        protected abstract Task<HttpResponseResult> _GetBalances(bool force = false);
        protected abstract Task<HttpResponseResult> _GetCandles(string symbol, CandlePeriod p, int limit, bool force = false);
        protected abstract Task<HttpResponseResult> _GetOrderbooks(string symbol, bool force = false);
        protected abstract Task<HttpResponseResult> _GetOrders(string symbol, bool force = false);
        protected abstract Task<HttpResponseResult> _GetOwnTrades(string symbol, bool force = false);
        protected abstract Task<HttpResponseResult> _GetSymbol(string symbol, bool force = false);
        protected abstract Task<HttpResponseResult> _GetSymbols(bool force = false);
        protected abstract Task<HttpResponseResult> _GetTrades(string symbol, bool force = false);


        protected virtual int RateQuantityLeft
        {
            get
            {
                return this.RateQuantityMax - requests.Where(x => x.time > DateTime.Now.Add(-this.rateLimitInterval)).Count();
            }
        }


        public virtual async Task<HttpResponseResult> GetBalance(string currency, bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetBalance(currency, force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }

        public virtual async Task<HttpResponseResult> GetBalances(bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetBalances(force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }

        public virtual async Task<HttpResponseResult> GetCandles(string symbol, CandlePeriod p, int limit, bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetCandles(symbol, p, limit, force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }

        public virtual async Task<HttpResponseResult> GetOrderbooks(string symbol, bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetOrderbooks(symbol, force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }

        public virtual async Task<HttpResponseResult> GetOrders(string symbol, bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetOrders(symbol, force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }

        public virtual async Task<HttpResponseResult> GetOwnTrades(string symbol, bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetOwnTrades(symbol, force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }

        public virtual async Task<HttpResponseResult> GetSymbol(string symbol, bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetSymbol(symbol, force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }

        public virtual async Task<HttpResponseResult> GetSymbols(bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetSymbols(force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }

        public virtual async Task<HttpResponseResult> GetTrades(string symbol, bool force = false)
        {
            if (force == true || this.RateQuantityLeft > 5)
            {
                return await _GetTrades(symbol, force);
            }
            else
            {
                throw new Exception("Too many requests");
            }
        }
    }
}
