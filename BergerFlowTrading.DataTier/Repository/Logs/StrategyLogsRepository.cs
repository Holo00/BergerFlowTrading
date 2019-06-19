using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model.Logs;
using BergerFlowTrading.Shared.DTO.Data.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.DataTier.Repository.Logs
{
    public class StrategyLogsRepository : RepositoryBase<StrategyLogsDTO, StrategyLog>
    {
        public StrategyLogsRepository(ApplicationDbContext ctxt, IMapper mapper) : base(ctxt, mapper)
        {
            this.usualIncludes.Add("StrategyRuns");
        }

        public async Task<List<StrategyLogsDTO>> GetByRunID(int runID, string userId, int? fromLast = null)
        {
            var result = this.Query(x => x.StrategyRuns_ID == runID && x.StrategyRuns.PlatformJob.User_ID == userId && (fromLast == null || x.ID > fromLast)).ToList();
            return mapper.Map<List<StrategyLog>, List<StrategyLogsDTO>>(result);
        }
    }
}
