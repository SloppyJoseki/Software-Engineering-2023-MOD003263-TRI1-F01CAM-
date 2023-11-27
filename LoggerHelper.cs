using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginInterface
{
    internal class LoggerHelper
    {
        // A helper class to allow for the use of both types of loggers
        private static LoggerBase logger = null;
        public static void Log(Constants_Functions.LogEndpoint logEndpoint, string message)
        {
            switch (logEndpoint)
            {
                case Constants_Functions.LogEndpoint.File:
                    logger = new FileLogger();
                    logger.Log(message);
                    break;
                case Constants_Functions.LogEndpoint.Database:
                    logger = new DBLogger();
                    logger.Log(message);
                    break;
                default:
                    return;
            }
        }
    }
}
