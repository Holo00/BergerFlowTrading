using BergerFlowTrading.DataTier.Repository;
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
    public interface IPlatformLoggerService
    {
        Task Log(string userId, string description, eventType eventType = eventType.Info, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task LogException(string userId, Exception exception, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
    }



    public abstract class PlatformLoggerServiceBase<TLogDTO, TLog, TRepository> : IPlatformLoggerService
        where TLogDTO : BaseLogDTO, new()
        where TLog : BaseLog
        where TRepository : RepositoryBase<TLogDTO, TLog>
    {
        protected RepositoryBase<TLogDTO, TLog> repo { get; set; }

        public PlatformLoggerServiceBase(RepositoryBase<TLogDTO, TLog> repo)
        {
            this.repo = repo;
        }

        public virtual async Task Log(string userId, string description, eventType eventType = eventType.Info, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            TLogDTO dto = this.CreateFromLog(userId, description, eventType, DetailLevelKeyword, lineNumber, caller);
            await this.repo.Insert(dto, userId);
        }

        public virtual async Task LogException(string userId, Exception exception, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            TLogDTO dto = this.CreateFromException(userId, exception, DetailLevelKeyword, lineNumber, caller);
            await this.repo.Insert(dto, userId);
        }

        protected TLogDTO CreateFromLog(string userId, string description, eventType eventType = eventType.Info, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return new TLogDTO()
            {
                Class = caller,
                Description = description,
                DetailLevelKeyword = DetailLevelKeyword,
                eventType = eventType,
                LineNumber = lineNumber,
                Timestamp = DateTime.UtcNow,
            };
        }

        protected TLogDTO CreateFromException(string userId, Exception exception, string DetailLevelKeyword = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return new TLogDTO()
            {
                Class = caller,
                Description = exception.ToString(),
                ExceptionMessage = exception.Message,
                InnerException = exception.InnerException != null ? exception.InnerException.ToString() : "",
                StackTrace = exception.StackTrace,
                DetailLevelKeyword = DetailLevelKeyword,
                eventType = eventType.Exception,
                LineNumber = lineNumber,
                Timestamp = DateTime.UtcNow,
            };
        }
    }
}
