using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public abstract class SpotExchange_API : SpotExchangeBase
    {
        public SpotExchange_API(bool ObserveAllSymbolsMode, bool ObserveAllBalancesMode, ExchangeDTO exchangeSettings, UserExchangeSecretDTO secrets, ExchangeLogService logService)
            : base(ObserveAllSymbolsMode, ObserveAllBalancesMode, exchangeSettings, secrets, logService)
        {
        }


        protected override async Task ObserveSymbol_All(CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.LoadSymbols();
                }
                catch (Exception ex)
                {
                    logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(30000);
                }
            }
        }

        protected override async Task ObserveSymbol_All_Stop(CancellationTokenSource token)
        {
            token.Cancel();
        }


        protected override async Task ObserveSymbol_One(string symbol, CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.RefreshSymbol(symbol);
                }
                catch (Exception ex)
                {
                    logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(30000);
                }
            }
        }

        protected override async Task ObserveSymbol_One_Stop(string symbol, CancellationTokenSource token)
        {
            token.Cancel();
        }


        protected override async Task ObserveBalance_All(CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.LoadBalances();
                }
                catch (Exception ex)
                {
                    this.logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(30000);
                }
            }
        }

        protected override async Task ObserveBalance_All_Stop(CancellationTokenSource token)
        {
            token.Cancel();
        }



        protected override async Task ObserveBalance_One(string currency, CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.RefreshBalance(currency);
                }
                catch (Exception ex)
                {
                    this.logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(30000);
                }
            }
        }

        protected override async Task ObserveBalance_One_Stop(string currency, CancellationTokenSource token)
        {
            token.Cancel();
        }



        protected override async Task ObserveOrders(string symbol, CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.RefreshOrders(symbol);
                }
                catch (Exception ex)
                {
                    this.logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(30000);
                }
            }
        }

        protected override async Task ObserveOrders_Stop(string symbol, CancellationTokenSource token)
        {
            token.Cancel();
        }



        protected override async Task ObserveOrderbook(string symbol, CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.RefreshOrderbooks(symbol);
                }
                catch (Exception ex)
                {
                    this.logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(30000);
                }
            }
        }

        protected override async Task ObserveOrderbook_Stop(string symbol, CancellationTokenSource token)
        {
            token.Cancel();
        }



        protected override async Task ObserveTrades(string symbol, CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.RefreshTrades(symbol);
                }
                catch (Exception ex)
                {
                    this.logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(30000);
                }
            }
        }

        protected override async Task ObserveTrades_Stop(string symbol, CancellationTokenSource token)
        {
            token.Cancel();
        }

        protected override async Task ObserveOwnTrades(string symbol, CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.RefreshOwnTrades(symbol);
                }
                catch (Exception ex)
                {
                    this.logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(30000);
                }
            }
        }

        protected override async Task ObserveOwnTrades_Stop(string symbol, CancellationTokenSource token)
        {
            token.Cancel();
        }



        protected override async Task ObserveCandles(string symbol, CandlePeriod p, CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await this.RefreshCandles(symbol, p, 100);
                }
                catch (Exception ex)
                {
                    this.logService.LogException(this.exchangeSettings.ID, null, ex);
                }
                finally
                {
                    await Task.Delay(60000);
                }
            }
        }

        protected override async Task ObserveCandles_Stop(string symbol, CandlePeriod p, CancellationTokenSource token)
        {
            token.Cancel();
        }
    }
}
