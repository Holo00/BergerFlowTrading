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
        private readonly ILoggingService logger;

        public ExchangeFactory(UserExchangeSecretRepository repoExchangeSecrets, ILoggingService logger)
        {
            this.repoExchangeSecrets = repoExchangeSecrets;
            this.logger = logger;
        }


        public async Task<ISpotExchangeFacade> CreateExchanges(ExchangeDTO exx)
        {
            ISpotExchangeFacade fac = null;

            if (exx.Name == ExchangeName.Binance.ToString() && this.exchanges.FirstOrDefault(x => x.ExchangeName == ExchangeName.Binance) != null)
            {
                UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID.Value);
                fac = new BinanceExchange(this.logger, exx, secrets);
            }
            else if (exx.Name == ExchangeName.HitBTC.ToString() && this.exchanges.FirstOrDefault(x => x.ExchangeName == ExchangeName.Binance) != null)
            {
                UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID.Value);
                fac = new HitBTCExchange(this.logger, exx, secrets);
            }
            else if (exx.Name == ExchangeName.KuCoin.ToString() && this.exchanges.FirstOrDefault(x => x.ExchangeName == ExchangeName.Binance) != null)
            {
                UserExchangeSecretDTO secrets = await this.repoExchangeSecrets.GetByExchangeId(exx.ID.Value);
                fac = new KuCoinExchange(this.logger, exx, secrets);
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
    }
}
