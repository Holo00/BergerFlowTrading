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
    public class ExchangeLogService : PlatformLoggerServiceBase<ExchangeLogsDTO, ExchangeLogs, ExchangeLogsRepository>
    {
        public ExchangeLogService(ExchangeLogsRepository repo) : base(repo)
        {
        }

        public async Task Log(int PlatformJobID, int ExchangeID, string userId, string description, eventType eventType = eventType.Info, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            ExchangeLogsDTO dto = this.CreateFromLog(userId, description, eventType, DetailLevelKeyword, lineNumber, caller);
            dto.Exchange_ID = ExchangeID;
            dto.PlatformJob_ID = PlatformJobID;
            await this.repo.Insert(dto, userId);
        }

        public async Task LogException(int PlatformJobID, int ExchangeID, string userId, Exception exception, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            ExchangeLogsDTO dto = this.CreateFromException(userId, exception, DetailLevelKeyword, lineNumber, caller);
            dto.Exchange_ID = ExchangeID;
            dto.PlatformJob_ID = PlatformJobID;
            await this.repo.Insert(dto, userId);
        }
    }
}
