using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LoginInterface
{
    internal class Constants_Functions
    {
        public enum LogEndpoint
        {
            File, Database
        }

        // This query checks the UserData table for a spesific email and is case sensitive
        public static string emailQuery = "SELECT Salt FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";

        // This query searches the UserData table for the password associated with an email
        public static string passwordQuery = "SELECT Password FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";

        // This query inserts an email password and salt into the UserData table
        public static string userInsertQuery = "INSERT INTO UserData (Email, Password, Salt) VALUES (@username, @password, @userSalt)";

        // This query checks if an email already exsists in the UserData table
        public static string checkEmailQuery = "SELECT 1 FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";

        // This query saves a file name, extension and data to the db table Files
        public static string saveFileQuery = "INSERT INTO Files (File_Data, File_Extension, File_Name) VALUES(@fileData, @extension, @fileName)";

        // This query if for finding files in the Files table by ID
        public static string fileDataQuery = "SELECT Id, File_Name, File_Extension FROM Files";

        // This query is for updating the Log file saved in the UserData table
        public static string SaveLogFileQuery = "UPDATE UserData SET File_Data = @fileData WHERE Email COLLATE Latin1_General_CS_AS = @Email";
        public static string logFilePath
        {
            // Allows the log file to be named after the specific user that is logged in 
            get
            {
                return LoggedInAs.GetInstanceOfLoggedInAs().currentUserEmail + ".txt";
            }
        }
        public static string logInformation(params object[] args)
        {
            // Get the current date and time as a string
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            // Using stackTrace to get the name of the method being called to add to the log
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase method = stackFrame.GetMethod();

            // building the log message
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append(method.Name);

            // Appending any arguments passed in
            if (args.Length > 0)
            {
                logMessage.Append(" (");

                for (int i = 0; i < args.Length; i++)
                {
                    logMessage.Append(args[i].ToString());
                    if (i < args.Length - 1)
                    {
                        logMessage.Append(", ");
                    }
                }
            }
            
            logMessage.Append(")");
            logMessage.Append(" " + formattedDateTime);
 
            return logMessage.ToString();
        }

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
