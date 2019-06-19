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
    public class PlatformJobsRepository : RepositoryByUser<PlatformJobsDTO, PlatformJob>
    {
        public PlatformJobsRepository(ApplicationDbContext ctxt, IMapper mapper) : base(ctxt, mapper)
        {
        }

        public async Task<PlatformJobsDTO> GetLast(string userId)
        {
            List<string> includes = this.usualIncludes;
            includes.Add("PlatformLogs");
            includes.Add("StrategyRuns");

            PlatformJob job = this.Query(x => x.User_ID == userId, includes).OrderByDescending(x => x.StartTime).FirstOrDefault();
            return mapper.Map<PlatformJob, PlatformJobsDTO>(job);
        }

        //public async Task GetFromDates(string userId, string startDate, string endDate)
        //{
        //    this.Query(x => x.StartTime.Date.ToString() >= startDate || x.StartTime.Date.ToString() )
        //}
    }
}
