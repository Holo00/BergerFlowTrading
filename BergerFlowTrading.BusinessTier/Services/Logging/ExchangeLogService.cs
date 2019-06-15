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
        private int ExchangeID { get; set; }
        private int PlatformJobID { get; set; }

        public ExchangeLogService(ExchangeLogsRepository repo, int ExchangeID, int PlatformJobID) : base(repo)
        {
            this.ExchangeID = ExchangeID;
            this.PlatformJobID = PlatformJobID;
        }

        public override async Task Log(string userId, string description, eventType eventType = eventType.Info, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            ExchangeLogsDTO dto = this.CreateFromLog(userId, description, eventType, DetailLevelKeyword, lineNumber, caller);
            dto.Exchange_ID = this.ExchangeID;
            dto.PlatformJob_ID = this.PlatformJobID;
            await this.repo.Insert(dto, userId);
        }

        public override async Task LogException(string userId, Exception exception, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            ExchangeLogsDTO dto = this.CreateFromException(userId, exception, DetailLevelKeyword, lineNumber, caller);
            dto.Exchange_ID = this.ExchangeID;
            dto.PlatformJob_ID = this.PlatformJobID;
            await this.repo.Insert(dto, userId);
        }
    }
}
