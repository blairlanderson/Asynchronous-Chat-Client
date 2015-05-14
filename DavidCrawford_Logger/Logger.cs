using Interfaces;
using log4net;
using log4net.Appender;
using log4net.Config;
using System;

namespace DavidCrawford_Logger
{
    //log4net tutorial
    //http://www.codeproject.com/Articles/8245/A-Brief-Introduction-to-the-log-net-logging-librar

    public class Logger : ILoggingService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Logger));

        public void Log(string message)
        {
            BasicConfigurator.Configure();
            logger.Info(message);
        }

    }
}
