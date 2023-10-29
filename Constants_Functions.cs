using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LoginInterface
{
    internal class Constants_Functions
    {
        public string emailQuery = "SELECT Salt FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";
        public string passwordQuery = "SELECT Password FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";
        public string userInsertQuery = "INSERT INTO UserData (Email, Password, Salt) VALUES (@username, @password, @userSalt)";
        public string checkEmailQuery = "SELECT 1 FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";

        public byte[] HashPassword(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000))
            {
                return pbkdf2.GetBytes(32);
            }
        }

    }
}
