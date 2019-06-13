using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model.Logs;
using BergerFlowTrading.Shared.DTO.Data.Logs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.DataTier.Repository.Logs
{
    public class ExchangeLogsRepository : RepositoryBase<ExchangeLogsDTO, ExchangeLogs>
    {
        public ExchangeLogsRepository(ApplicationDbContext ctxt, IMapper mapper) : base(ctxt, mapper)
        {
            this.usualIncludes.Add("PlatformJob");
            this.usualIncludes.Add("Exchange");
        }
    }
}
