using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.Logging
{
    public interface ILoggingService
    {
        void Log(string message);
        void Log(Exception ex);
        void Log(List<Exception> ex);
    }

    public class LoggingService : ILoggingService
    {
        public void Log(string message)
        {
            throw new NotImplementedException();
        }

        public void Log(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Log(List<Exception> ex)
        {
            throw new NotImplementedException();
        }
    }
}
