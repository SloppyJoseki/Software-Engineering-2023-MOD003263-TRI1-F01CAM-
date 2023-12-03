using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginInterface
{
    class LoggedInAs
    {
        // Another singleton design to keep track of who is logged in
        private static LoggedInAs _instance;
        private LoggedInAs() { }
        public string CurrentUserEmail
        {
            // The email of whoever is logged in is an attribute
            get;
            set;
        }
        public static LoggedInAs GetInstanceOfLoggedInAs()
        {
            // Lets an instance of the class be called to get information
            if (_instance == null)
            {
                _instance = new LoggedInAs();

                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation());
                return _instance;
            }
            LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
            Constants_Functions.LogInformation());
            return _instance;
        }
    }
}
