using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model;
using BergerFlowTrading.Shared.DTO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.DataTier.Repository
{
    public class LimitStrategy4SettingsRepository : RepositoryByUser<LimitArbitrageStrategy4SettingsDTO, LimitArbitrageStrategy4Settings>
    {
        public LimitStrategy4SettingsRepository(ApplicationDbContext ctxt, IMapper mapper) : base(ctxt, mapper)
        {
            this.usualIncludes.Add("Exchange_1");
            this.usualIncludes.Add("Exchange_2");
        }

        public async Task<List<LimitArbitrageStrategy4SettingsDTO>> GetActivesOrBalanceManage()
        {
            var result = this.Query(x => x.ManagementBalanceON || x.Active, this.usualIncludes, false).ToList();
            return result.Select(x => mapper.Map<LimitArbitrageStrategy4Settings, LimitArbitrageStrategy4SettingsDTO>(x)).ToList();
        }
    }
}
