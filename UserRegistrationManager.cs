using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginInterface
{
    internal class UserRegistrationManager
    {
        public void RegisterUser(string username, string password)
        {
            DbConnector.GetInstanceOfDBConnector().SendEmail(username);

        }
    }
}
