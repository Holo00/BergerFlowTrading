using BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Exchanges.Spot;
using BergerFlowTrading.DataTier.Repository;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.AutomatedTrading.Strategy
{
    public class StrategySettingsFactory
    {
        private readonly LimitStrategy4SettingsRepository repoLimit4;

        public StrategySettingsFactory(LimitStrategy4SettingsRepository repoLimit4)
        {
            this.repoLimit4 = repoLimit4;
        }

        public async Task<List<IStrategySettingDTO>> LoadStrategies()
        {
            List<Task<IEnumerable<IStrategySettingDTO>>> tasks = new List<Task<IEnumerable<IStrategySettingDTO>>>();

            tasks.Add(LoadLimit4CurrentStrategies());

            return (await Task.WhenAll(tasks)).SelectMany(x => x).ToList();
        }

        private async Task<IEnumerable<IStrategySettingDTO>> LoadLimit4CurrentStrategies()
        {
            List<LimitArbitrageStrategy4SettingsDTO> limit4ToExecute = await this.repoLimit4.GetActivesOrBalanceManage();

            List<ExchangeDTO> exchanges = limit4ToExecute.Select(x => x.Exchange_1)
                                            .Union(limit4ToExecute.Select(x => x.Exchange_2))
                                            .Distinct().ToList();

            return limit4ToExecute.Cast<IStrategySettingDTO>().ToList();
        }
    }
}
