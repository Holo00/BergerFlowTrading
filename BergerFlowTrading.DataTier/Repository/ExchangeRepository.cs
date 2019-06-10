using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model;
using BergerFlowTrading.Shared.DTO.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.DataTier.Repository
{
    public class ExchangeRepository: RepositoryBase<ExchangeDTO, Exchange>
    {
        public ExchangeRepository(ApplicationDbContext ctxt, IMapper mapper): base(ctxt, mapper)
        {
        }

        protected override async Task<bool> Insert(Exchange entity, bool andSave = true)
        {
            return await base.Insert(entity, andSave);
        }

        protected override async Task<bool> Insert(IEnumerable<Exchange> entities, bool andSave = true)
        {
            return await base.Insert(entities, andSave);
        }

        protected override async Task<bool> Update(Exchange entity, bool andSave = true)
        {
            return await base.Update(entity, andSave);
        }

        protected override async Task<bool> Update(IEnumerable<Exchange> entities, bool andSave = true)
        {
            return await base.Update(entities, andSave);
        }
    }
}
