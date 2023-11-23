﻿using System;
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
            get;
            set;
        }
        public static LoggedInAs GetInstanceOfLoggedInAs()
        {
            if (_instance == null)
            {
                _instance = new LoggedInAs();
                return _instance;
            }
            return _instance;
        }
    }
}
