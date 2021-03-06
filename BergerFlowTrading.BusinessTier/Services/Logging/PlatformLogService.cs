﻿using BergerFlowTrading.DataTier.Repository.Logs;
using BergerFlowTrading.Model.Logs;
using BergerFlowTrading.Shared.DTO.Data.Logs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.BusinessTier.Services.Logging
{
    public class PlatformLogService : PlatformLoggerServiceBase<PlatformLogsDTO, PlatformLogs, PlatformLogsRepository>
    {
        public PlatformLogService(PlatformLogsRepository repo) : base(repo)
        {
        }

        public override async Task Log(int PlatformJobID, string userId, string description, eventType eventType = eventType.Info, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            PlatformLogsDTO dto = this.CreateFromLog(userId, description, eventType, DetailLevelKeyword, lineNumber, caller);
            dto.PlatformJob_ID = PlatformJobID;
            await this.repo.Insert(dto, userId);
        }

        public override async Task LogException(int PlatformJobID, string userId, Exception exception, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            PlatformLogsDTO dto = this.CreateFromException(userId, exception, DetailLevelKeyword, lineNumber, caller);
            dto.PlatformJob_ID = PlatformJobID;
            await this.repo.Insert(dto, userId);
        }
    }
}
