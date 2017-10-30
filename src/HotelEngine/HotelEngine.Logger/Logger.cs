using System;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace HotelEngine.Logger
{
    public class Logger
    {
        private ILoggerFactory _loggerFactory;

        public Logger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Create log entry
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Log(LogLevel logLevel, string msg, params object[] args)
        {            
            ILogger logger=_loggerFactory.CreateLogger("Log");
            if (logLevel == LogLevel.Information)
                logger.LogInformation(msg, args);
            else if (logLevel == LogLevel.Warning)
                logger.LogWarning(msg, args);
            else if (logLevel == LogLevel.Error)
                logger.LogWarning(msg, args);            
        }
    }
}
