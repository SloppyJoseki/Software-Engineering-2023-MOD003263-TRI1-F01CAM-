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
    class DbConnector
    {
        private static DbConnector _instance;

        private String connectionString;

        private SqlConnection connectToDb;

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
                        MessageBox.Show("File saved bossman!");
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
    }
}

