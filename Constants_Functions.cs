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
        // This query checks the UserData table for a spesific email and is case sensitive
        public static string emailQuery = "SELECT Salt FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";
        // This query searches the UserData table for the password associated with an email
        public static string passwordQuery = "SELECT Password FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";
        // This query inserts an email password and salt into the UserData table
        public static string userInsertQuery = "INSERT INTO UserData (Email, Password, Salt) VALUES (@username, @password, @userSalt)";
        // This query checks if an email already exsists in the UserData table
        public static string checkEmailQuery = "SELECT 1 FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";
        public static string saveFileQuery = "INSERT INTO Files (File_Data, File_Extension, File_Name) VALUES(@fileData, @extension, @fileName)";

        public static byte[] HashPassword(string password, byte[] salt)
        {
            // This function uses pbkdf2 with a salt in order to hash the users password
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000))
            {
                return pbkdf2.GetBytes(32);
            }
        }
    }
}
