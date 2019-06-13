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
    }
}
