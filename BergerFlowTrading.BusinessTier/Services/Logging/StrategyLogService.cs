using BergerFlowTrading.DataTier.Repository.Logs;
using BergerFlowTrading.Model.Logs;
using BergerFlowTrading.Shared.DTO.Data.Logs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.Logging
{
    public class StrategyLogService : PlatformLoggerServiceBase<StrategyLogsDTO, StrategyLog, StrategyLogsRepository>
    {
        private int StrategyRuns_ID { get; set; }

        public StrategyLogService(StrategyLogsRepository repo, int StrategyRuns_ID) : base(repo)
        {
            this.StrategyRuns_ID = StrategyRuns_ID;
        }

        public override async Task Log(string userId, string description, eventType eventType = eventType.Info, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            StrategyLogsDTO dto = this.CreateFromLog(userId, description, eventType, DetailLevelKeyword, lineNumber, caller);
            dto.StrategyRuns_ID = this.StrategyRuns_ID;
            await this.repo.Insert(dto, userId);
        }

        public override async Task LogException(string userId, Exception exception, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            StrategyLogsDTO dto = this.CreateFromException(userId, exception, DetailLevelKeyword, lineNumber, caller);
            dto.StrategyRuns_ID = this.StrategyRuns_ID;
            await this.repo.Insert(dto, userId);
        }
    }
}
