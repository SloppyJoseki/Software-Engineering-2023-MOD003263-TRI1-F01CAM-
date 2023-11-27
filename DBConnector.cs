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
using System.Data.Common;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LoginInterface
{
    class DbConnector : IDBConnector
    {
        readonly private List<IDbObserver> observers = new List<IDbObserver>();
        private static DbConnector _instance;
        readonly private String connectionString;


        // Singleton design methods 
        private DbConnector()
        {
            connectionString = Properties.Settings.Default.DBExcludingUsersConnectionString;
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
        //method to load table from DBExcludingUsers
        public DataSet getDataSet(string sqlQuery)
        {
            //create a dataset
            DataSet ds = new DataSet();
            try 
            {
                using (SqlConnection connToDB2 = new SqlConnection(connectionString))
                {
                    //open connection
                    connToDB2.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connToDB2);
                    //fills dataset
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            
        }
        //method to add peramiters from updateDB Form in DBExcludingUsers
        public void addToDB(string sqlQuery, int data0, string data1, string data2, int data3, int data4, string data5)
        {
            using (SqlConnection connToDB2 = new SqlConnection(connectionString))
            {
                connToDB2.Open();

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connToDB2);

                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.Add(new SqlParameter("Company_ID", data0));
                sqlCommand.Parameters.Add(new SqlParameter("Company_name", data1));
                sqlCommand.Parameters.Add(new SqlParameter("Company_website", data2));
                sqlCommand.Parameters.Add(new SqlParameter("Company_established", data3));
                sqlCommand.Parameters.Add(new SqlParameter("No_of_Employees", data4));
                sqlCommand.Parameters.Add(new SqlParameter("Internal_Professional_Services", data5));

                //execution of command
                sqlCommand.ExecuteNonQuery();


            }
        }
        //method to update peramiters from updateDB Form in DBExcludingUsers
        public void updateToDB(string sqlQuery, int data0, string data1, string data2, int data3, int data4, string data5)
        {
            using (SqlConnection connToDB2 = new SqlConnection(connectionString))
            {
                connToDB2.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connToDB2))
                {
                    command.Parameters.AddWithValue("@CompanyName", data1);
                    command.Parameters.AddWithValue("@CompanyWebsite", data2);
                    command.Parameters.AddWithValue("@CompanyEstablished", data3);
                    command.Parameters.AddWithValue("@NumberOfEmployees", data4);
                    command.Parameters.AddWithValue("@InternalServices", data5);
                    command.Parameters.AddWithValue("@CompanyID", data0);

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();


                }
            }
        }
        // Observer design methods
        private string[] GetColumnNames()
        {
            DataSet schema = DbConnector.GetInstanceOfDBConnector().getDataSet("SELECT TOP 0 * FROM Software");
            return schema.Tables[0].Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
        }
        public DataSet SearchAndFiltering(string searchText)
        {
            // Construct base SQL query
            string query = "SELECT * FROM Software WHERE ";

            // GetColumnNames method to retrieve column names
            string[] columnNames = GetColumnNames();

            for (int i = 0; i < columnNames.Length; i++)
            {
                if (i > 0)
                {
                    query += " OR ";
                }
                query += $"CONVERT(NVARCHAR(MAX), [{columnNames[i]}]) LIKE @searchText";
            }

            // Add parameter to query
            SqlParameter parameter = new SqlParameter("@searchText", searchText);

            // Execute query and fill the DataGridView
            try
            {
                DataSet filteredData = new DataSet();
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameter to query
                            command.Parameters.Add(new SqlParameter("@searchText", searchText));

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                // Fill the DataTable with the results
                                adapter.Fill(filteredData);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }

                if (filteredData != null && filteredData.Tables.Count > 0)
                {
                    return filteredData;
                }
                else
                {
                    // No matching data found
                    Console.WriteLine($"No matching data found for search: {searchText}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., database connection issues
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public void AddDbObserver(IDbObserver observer)
        {
            observers.Add(observer);
        }
        public void RemoveDbObserver(IDbObserver observer)
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
                using (SqlCommand command = new SqlCommand(Constants_Functions.ObserversQuery, connection))
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
                String emailQuery = Constants_Functions.EmailQuery;

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
                string passwordQuery = Constants_Functions.PasswordQuery;

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
        public bool IsEmailTaken(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string checkEmailQuery = Constants_Functions.CheckEmailQuery;

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
                string userInsertQuery = Constants_Functions.UserInsertQuery;

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
                String FileQuery = Constants_Functions.SaveFileQuery;

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
                        LoggedInAs.GetInstanceOfLoggedInAs().CurrentUserEmail + " it is named: " +
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
            string fileDataQuery = Constants_Functions.FileDataQuery;

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

                String FileQuery = Constants_Functions.SaveFileQuery;

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

