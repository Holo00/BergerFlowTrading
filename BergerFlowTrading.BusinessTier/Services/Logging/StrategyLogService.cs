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
        public StrategyLogService(StrategyLogsRepository repo) : base(repo)
        {
        }

        public override async Task Log(int StrategyRuns_ID,string userId, string description, eventType eventType = eventType.Info, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            StrategyLogsDTO dto = this.CreateFromLog(userId, description, eventType, DetailLevelKeyword, lineNumber, caller);
            dto.StrategyRuns_ID = StrategyRuns_ID;
            await this.repo.Insert(dto, userId);
        }

        public override async Task LogException(int StrategyRuns_ID, string userId, Exception exception, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            StrategyLogsDTO dto = this.CreateFromException(userId, exception, DetailLevelKeyword, lineNumber, caller);
            dto.StrategyRuns_ID = StrategyRuns_ID;
            await this.repo.Insert(dto, userId);
        }
    }
}
