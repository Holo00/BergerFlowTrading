using BergerFlowTrading.BusinessTier.Services.Encryption;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
using BergerFlowTrading.Shared.Http;
using BergerFlowTrading.Shared.TradingUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Kucoin
{
    public class KuCoinApi : SpotExchangeApiBase
    {
        public KuCoinApi(ExchangeDTO exchangeSettings, ILoggingService logger, UserExchangeSecretDTO secrets) : base(exchangeSettings, logger, secrets)
        {

        }

        protected async Task<HttpResponseResult> ExecuteRequest(string endpoint, string queryString, string verb, object body = null, string contentType = "application/json", List<(string, string)> headers = null, bool logCalls = false)
        {
            try
            {
                if (headers == null)
                {
                    headers = new List<(string, string)>();
                }

                //authentication
                List<string> lst = queryString.Split('&').ToList();

                if (verb == "POST" && contentType == "application/x-www-form-urlencoded" && body != null)
                {
                    foreach (KeyValuePair<string, string> val in (List<KeyValuePair<string, string>>)body)
                    {
                        lst.Add($"{val.Key}={val.Value}");
                    }
                }

                string exQueryString = lst.OrderBy(x => x).Aggregate((x, y) => x + "&" + y);

                long nonce = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).Ticks / 10000;
                string sign64Str = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{endpoint}/{nonce.ToString()}/{exQueryString}"));
                string signature = SignHelper.CalculateSignature_HMACSHA256(sign64Str, secrets.Api_Secret);

                headers.Add(new ValueTuple<string, string>("KC-API-KEY", secrets.Api_ID));
                headers.Add(new ValueTuple<string, string>("KC-API-NONCE", nonce.ToString()));
                headers.Add(new ValueTuple<string, string>("KC-API-SIGNATURE", signature));

                return await base.ExecuteRequest(endpoint, queryString, verb, body, contentType, headers);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        protected override async Task<HttpResponseResult> _GetBalance(string currency, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override async Task<HttpResponseResult> _GetBalances(bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override async Task<HttpResponseResult> _GetCandles(string symbol, CandlePeriod p, int limit, bool force = false)
        {
            DateTime from = DateTime.UtcNow;
            DateTime to = DateTime.UtcNow;

            string url = $"/v1/open/chart/history";
            string queryString = $"symbol={Currency.GetBaseFromSymbol(symbol)}-{Currency.GetQuoteFromSymbol(symbol)}";

            switch (p)
            {
                case CandlePeriod.M1:
                    queryString += $"&resolution=1";
                    from = from.AddMinutes(-limit);
                    break;
                case CandlePeriod.M5:
                    queryString += $"&resolution=5";
                    from = from.AddMinutes(-limit * 5);
                    break;
                case CandlePeriod.M15:
                    queryString += $"&resolution=15";
                    from = from.AddMinutes(-limit * 15);
                    break;
                case CandlePeriod.M30:
                    queryString += $"&resolution=30";
                    from = from.AddMinutes(-limit * 30);
                    break;
                case CandlePeriod.H1:
                    queryString += $"&resolution=60";
                    from = from.AddMinutes(-limit * 60);
                    break;
                case CandlePeriod.D1:
                    queryString += $"&resolution=D";
                    from = from.AddDays(-limit);
                    break;
                case CandlePeriod.W1:
                    queryString += $"&resolution=W";
                    from = from.AddDays(-limit * 7);
                    break;
            }

            queryString += $"&from={(from.Subtract(new DateTime(1970, 1, 1))).Ticks / 10000000}";
            queryString += $"&to={(to.Subtract(new DateTime(1970, 1, 1))).Ticks / 10000000}";

            string verb = "GET";

            return await this.ExecuteRequest(url, queryString, verb, null);
        }

        protected override async Task<HttpResponseResult> _GetOrderbooks(string symbol, bool force = false)
        {
            string url = $"/v1/open//orders";
            string queryString = $"symbol={Currency.GetBaseFromSymbol(symbol)}-{Currency.GetQuoteFromSymbol(symbol)}";
            string verb = "GET";

            return await this.ExecuteRequest(url, queryString, verb, null);
        }

        protected override async Task<HttpResponseResult> _GetOrders(string symbol, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override async Task<HttpResponseResult> _GetOwnTrades(string symbol, bool force = false)
        {
            throw new NotImplementedException();
        }

        protected override async Task<HttpResponseResult> _GetSymbol(string symbol, bool force = false)
        {
            string url = $"/v1/market/symbols";
            string queryString = $"symbol={Currency.GetBaseFromSymbol(symbol)}-{Currency.GetQuoteFromSymbol(symbol)}";
            string verb = "GET";

            return await this.ExecuteRequest(url, queryString, verb, null);
        }

        protected override async Task<HttpResponseResult> _GetSymbols(bool force = false)
        {
            try
            {
                string url = $"/v1/market/symbols";
                string queryString = $"";
                string verb = "GET";

                return await this.ExecuteRequest(url, queryString, verb, null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected override async Task<HttpResponseResult> _GetTrades(string symbol, bool force = false)
        {
            throw new NotImplementedException();
        }
    }
}
