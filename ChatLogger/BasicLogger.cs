using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace ChatLogger
{
    /// <summary>
    /// Custom Logging class
    /// </summary>
    public class BasicLogger : ILoggingService
    {
        string filename = DateTime.Now.ToString("yyyy-MM-dd-hh_mm") + ".log";
        /// <summary>
        /// Method to log conversation history to text file
        /// </summary>
        /// <param name="message">Message info to be logged.</param>
        public void Log(string message) 
        {
            //TODO NEW LOG FILE PER SESSION
            message = DateTime.Now.ToString("MM-dd-hh:mm") + ": " + message;
            System.IO.File.AppendAllText(filename, message + Environment.NewLine);
        }
    }//end Logger class
}
