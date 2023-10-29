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
            byte[] userSalt = null;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                String emailQuery = "SELECT Salt FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";

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
                string passwordQuery = "SELECT Password FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";

                using (SqlCommand sqlCommand = new SqlCommand(passwordQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", email));

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] storedPassword = (byte[])reader["Password"];
                            UserRegistrationManager userRegistrationManager = new UserRegistrationManager();
                            byte[] hashedPassword = (userRegistrationManager.HashPassword(password, salt));

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

        public void AddUserToDB(string username, string password)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            UserRegistrationManager userRegistrationManager = new UserRegistrationManager();
            byte[] userSalt = userRegistrationManager.GenerateSalt(16);
            byte[] securePassword = userRegistrationManager.HashPassword(password, userSalt);

            try
            {
                connection.Open();
                string userInsertQuery = "INSERT INTO UserData (Email, Password, Salt) VALUES (@username, @password, @userSalt)";

                using (SqlCommand sqlCommand = new SqlCommand(userInsertQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@username", username));
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
    }
}

