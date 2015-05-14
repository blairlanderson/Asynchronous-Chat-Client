using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;
using NLog;
namespace BlairAnderson_Logger
{

    //http://www.codeproject.com/Articles/10631/Introduction-to-NLog

    /// <summary>
    /// 
    /// </summary>
    public class NLogger : ILoggingService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public NLogger()
        {
            
        }

        /// <summary>
        /// logs according to nlog.config
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            logger.Info(message);
            
            //Console.WriteLine("Blah");
        }
    }
}
