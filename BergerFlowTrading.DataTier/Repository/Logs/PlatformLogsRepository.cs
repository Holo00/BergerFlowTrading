using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model.Logs;
using BergerFlowTrading.Shared.DTO.Data.Logs;

namespace BergerFlowTrading.DataTier.Repository.Logs
{
    public class PlatformLogsRepository : RepositoryBase<PlatformLogsDTO, PlatformLogs>
    {
        public PlatformLogsRepository(ApplicationDbContext ctxt, IMapper mapper) : base(ctxt, mapper)
        {
            this.usualIncludes.Add("PlatformJob");
        }

        public async Task<List<PlatformLogsDTO>> GetByJobID(int jobID, string userId, int? fromLast)
        {
            var result = this.Query(x => x.PlatformJob_ID == jobID && x.PlatformJob.User_ID == userId && (fromLast == null || x.ID > fromLast)).ToList();
            return mapper.Map<List<PlatformLogs>, List<PlatformLogsDTO>>(result);
        }
    }
}
