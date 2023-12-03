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
        public string filePath = Constants_Functions.LogFilePath;

        internal Constants_Functions Constants_Functions
        {
            get => default;
            set
            {
            }
        }

        public override void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message);
            }
        }
    }
    public class DBLogger : LoggerBase
    {
        // This needs to be all fixed and have a table created to save the data properly
        readonly string filePath = Constants_Functions.LogFilePath;
        readonly string email = LoggedInAs.GetInstanceOfLoggedInAs().CurrentUserEmail;
        public override void Log(string message)
        {
            DbConnector.GetInstanceOfDBConnector().SaveFile(filePath);
        }
    }
}
