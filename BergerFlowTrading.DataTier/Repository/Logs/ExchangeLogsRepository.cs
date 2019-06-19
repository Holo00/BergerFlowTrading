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
    public class ExchangeLogsRepository : RepositoryBase<ExchangeLogsDTO, ExchangeLogs>
    {
        public ExchangeLogsRepository(ApplicationDbContext ctxt, IMapper mapper) : base(ctxt, mapper)
        {
            this.usualIncludes.Add("PlatformJob");
            this.usualIncludes.Add("Exchange");
        }

        public async Task<List<ExchangeLogsDTO>> GetByExchangeID(int platformJobID, string userId, ExchangeLogType type, string filter, int? fromLast)
        {
            var result = this.Query(x =>
                                    x.PlatformJob_ID == platformJobID
                                    && x.PlatformJob.User_ID == userId
                                    && (type == ExchangeLogType.All
                                        || (type == ExchangeLogType.ByStrategy && x.Symbol == filter)
                                        || (type == ExchangeLogType.ByExchange && x.Exchange_ID == Convert.ToInt32(filter)))
                                    && (fromLast == null || x.ID > fromLast)    
                                    ).ToList();

            return mapper.Map<List<ExchangeLogs>, List<ExchangeLogsDTO>>(result);
        }
    }
}
