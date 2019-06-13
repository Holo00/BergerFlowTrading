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
    }
}
