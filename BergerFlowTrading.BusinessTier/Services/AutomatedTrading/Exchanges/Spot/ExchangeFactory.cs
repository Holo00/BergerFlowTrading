using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Binance;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.HitBTC;
using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot.Kucoin;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.DataTier.Repository;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot
{
    public class ExchangeFactory
    {
        public List<ISpotExchangeFacade> exchanges { get; private set; }

        private readonly UserExchangeSecretRepository repoExchangeSecrets;
        private readonly ExchangeLogService logger;

        public ExchangeFactory(UserExchangeSecretRepository repoExchangeSecrets, ExchangeLogService logger)
        {
            this.repoExchangeSecrets = repoExchangeSecrets;
            this.logger = logger;
        }

        public async Task<ISpotExchangeFacade> GetExchange(ExchangeDTO exx)
        {
            ISpotExchangeFacade ex = this.exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == exx.Name);

            if(ex != null)
            {
                return ex;
            }
            else
            {
                return await this.CreateExchanges(exx);
            }
        }

        public async Task<ISpotExchangeFacade> CreateExchanges(ExchangeDTO exx)
        {
            ISpotExchangeFacade fac = null;

            if (exx.Name == ExchangeName.Binance.ToString() && this.exchanges.FirstOrDefault(x => x.ExchangeName == ExchangeName.Binance) != null)
            {
                UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID);
                fac = new BinanceExchange(exx, secrets, logger);
            }
            else if (exx.Name == ExchangeName.HitBTC.ToString() && this.exchanges.FirstOrDefault(x => x.ExchangeName == ExchangeName.Binance) != null)
            {
                UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID);
                fac = new HitBTCExchange(exx, secrets, logger);
            }
            else if (exx.Name == ExchangeName.KuCoin.ToString() && this.exchanges.FirstOrDefault(x => x.ExchangeName == ExchangeName.Binance) != null)
            {
                UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID);
                fac = new KuCoinExchange(exx, secrets, logger);
            }

            if (fac != null)
            {
                this.exchanges.Add(fac);
            }
            else
            {
                fac = this.exchanges.FirstOrDefault(x => x.ExchangeName.ToString() == exx.Name);
            }

            return fac;
        }

        public void DisposeOf(ISpotExchangeFacade fac)
        {
            this.exchanges.Remove(fac);
            fac.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
