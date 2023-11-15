using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace LoginInterface
{
    class DbConnector : IDBConnector
    {
        private List<IdbObserver> observers = new List<IdbObserver>();
        private static DbConnector _instance;
        private String connectionString;

        // Singleton design methods 
        private DbConnector()
        {
            connectionString = Properties.Settings.Default.DBConnectionString;
        }
        public static DbConnector GetInstanceOfDBConnector()
        {
            if (_instance == null)
            {
                _instance = new DbConnector();
                return _instance;
            }
            return _instance;
        }

        // Observer design methods
        public void AddDbObserver(IdbObserver observer)
        {
            observers.Add(observer);
        }
        public void RemoveDbObserver(IdbObserver observer)
        {
            observers.Remove(observer);
        }
        public void NotifyDbObservers(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }

        // Get the list of observers
        public void GetObserverList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(Constants_Functions.observersQuery, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string email = reader["Email"].ToString();
                            AddDbObserver(new DbObserver(email));
                        }
                    }                   
                }
            }
        }

        // Db interaction methods 
        public byte[] CheckEmailGetSalt(string email)
        {
            // This function checks if the user entered email is in the database and if so 
            // returns the relevant salt
            byte[] userSalt = null;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                String emailQuery = Constants_Functions.emailQuery;

                using (SqlCommand sqlCommand = new SqlCommand(emailQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", email));

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userSalt = (byte[])reader["Salt"];
                            return userSalt;
                        }
                        else
                        {
                            MessageBox.Show("Email not found in the database soz");
                            return userSalt;
                        }
                    }   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return userSalt;
            }
        }
        public bool CheckUserPassword(string email, string password, byte[] salt)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string passwordQuery = Constants_Functions.passwordQuery;

                using (SqlCommand sqlCommand = new SqlCommand(passwordQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", email));

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] storedPassword = (byte[])reader["Password"];
                            byte[] hashedPassword = (Constants_Functions.HashPassword(password, salt));

                            // SequenceEqual compares each byte in the array wheras !=
                            // compares only object references
                            if (!storedPassword.SequenceEqual(hashedPassword))
                            {
                                MessageBox.Show("Password wrong sorry mate");
                                return false;
                            }
                            else
                            {
                                MessageBox.Show("I'm in");
                                return true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password wrong soz");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
        public bool isEmailTaken(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string checkEmailQuery = Constants_Functions.checkEmailQuery;

            try
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(checkEmailQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", email));
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }

        }
        public void AddUserToDB(string email, string password)
        {   
            SqlConnection connection = new SqlConnection(connectionString);
            UserRegistrationManager userRegistrationManager = new UserRegistrationManager();
            byte[] userSalt = userRegistrationManager.GenerateSalt(16);
            byte[] securePassword = Constants_Functions.HashPassword(password, userSalt);

            try
            {
                connection.Open();
                string userInsertQuery = Constants_Functions.userInsertQuery;

                using (SqlCommand sqlCommand = new SqlCommand(userInsertQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@username", email));
                    sqlCommand.Parameters.Add(new SqlParameter("@password", securePassword));
                    sqlCommand.Parameters.Add(new SqlParameter("@userSalt", userSalt));
                    sqlCommand.Parameters.Add(new SqlParameter("@Is_Admin", SqlDbType.Bit) { Value = false });
                    sqlCommand.Parameters.Add(new SqlParameter("@Is_Db_Observer", SqlDbType.Bit) { Value = false });
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Task failed successfully");
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void SaveFile(string filePath)
        {
            using (Stream stream = File.OpenRead(filePath))
            {
                byte[] fileData = new byte[stream.Length];
                stream.Read(fileData, 0, fileData.Length);

                String fileExtension = Path.GetExtension(filePath);
                String fileName = Path.GetFileName(filePath);
                String FileQuery = Constants_Functions.saveFileQuery;

                SqlConnection connection = new SqlConnection(connectionString);
                try
                { 
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(FileQuery, connection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@fileData", fileData));
                        sqlCommand.Parameters.Add(new SqlParameter("@extension", fileExtension));
                        sqlCommand.Parameters.Add(new SqlParameter("@fileName", fileName));
                        sqlCommand.ExecuteNonQuery();
                        string fileSavedNotification = "A new file has been saved by: " +
                        LoggedInAs.GetInstanceOfLoggedInAs().currentUserEmail + " it is named: " +
                        fileName;
                        NotifyDbObservers(fileSavedNotification);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        public void OpenFile(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string fileOpenQuery = "SELECT File_Name, File_Data, File_Extension FROM Files WHERE Id = @id";

                using (SqlCommand sqlCommand = new SqlCommand(fileOpenQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var name = reader["File_Name"].ToString();
                            var data = (byte[])reader["File_Data"];
                            var extn = reader["File_Extension"].ToString();
                            var newFileName = name.Replace(extn, DateTime.Now.ToString("ddMMyyyyhhmmss")) + extn;

                            File.WriteAllBytes(newFileName, data);
                            System.Diagnostics.Process.Start(newFileName);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public DataTable DisplayFileData()
        {
            string fileDataQuery = Constants_Functions.fileDataQuery;

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(fileDataQuery, connection))
                {
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt); 
                    return dt;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }
        public void SaveLogFile(string filePath, string email)
        {
            using (Stream stream = File.OpenRead(filePath))
            {
                byte[] fileData = new byte[stream.Length];
                stream.Read(fileData, 0, fileData.Length);

                String FileQuery = Constants_Functions.saveFileQuery;

                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(FileQuery, connection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@fileData", fileData));
                        sqlCommand.Parameters.Add(new SqlParameter("@Email", email));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}

