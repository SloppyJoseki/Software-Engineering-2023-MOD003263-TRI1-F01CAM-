using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginInterface
{
    public abstract class LoggerBase
    {
        // https://www.infoworld.com/article/2980677/implement-a-simple-logger-in-csharp.html
        // Was heavily used as a reference for the making of this logger
        public abstract void Log(string message);        
    }
    public class FileLogger : LoggerBase
    {
        // Sets the location the logger will save data to
        public string filePath = Constants_Functions.LogFilePath;

        public override void Log(string message)
        {
            // Overrides the base log function
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message);
            }
        }
    }
    public class DBLogger : LoggerBase
    {
        // Overrides the log function and saves it to the DB not in use for this program
        readonly string filePath = Constants_Functions.LogFilePath;
        public override void Log(string message)
        {
            DbConnector.GetInstanceOfDBConnector().SaveFile(filePath);
        }
    }
}
