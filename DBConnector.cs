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
        // Attributes
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
            // Get an instance of the singleton class letting the methods be used
            if (_instance == null)
            {
                _instance = new DbConnector();
                LoggerHelper.Log(Constants_Functions.LogEndpoint.File, Constants_Functions.LogInformation());
                return _instance;
            }
            LoggerHelper.Log(Constants_Functions.LogEndpoint.File, Constants_Functions.LogInformation());
            return _instance;
        }


        // Observer design methods 
        public void AddDbObserver(IDbObserver observer)
        {
            // Adds an observer to the list so they can be updated
            observers.Add(observer);
            LoggerHelper.Log(Constants_Functions.LogEndpoint.File, Constants_Functions.LogInformation());
        }
        public void RemoveDbObserver(IDbObserver observer)
        {
            // Removes an observer that no longer wishes to get updates
            observers.Remove(observer);
            LoggerHelper.Log(Constants_Functions.LogEndpoint.File, Constants_Functions.LogInformation());
        }
        public void NotifyDbObservers(string message)
        {
            // Loops through the list of observers and sends them each an email
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
            LoggerHelper.Log(Constants_Functions.LogEndpoint.File, Constants_Functions.LogInformation(message));
        }

        // Get the list of observers
        public void GetObserverList()
        {
            // On startup generates a list of all the observers from the database
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
                        LoggerHelper.Log(Constants_Functions.LogEndpoint.File, Constants_Functions.LogInformation());
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
                // Opens the connection to the DB
                connection.Open();
                // Gets the query from Constants_Functions
                String emailQuery = Constants_Functions.EmailQuery;

                // Runs the query
                using (SqlCommand sqlCommand = new SqlCommand(emailQuery, connection))
                {
                    // Adds the email in as a paramater
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", email));
                    
                    // Reads the data
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userSalt = (byte[])reader["Salt"];
                            LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                            Constants_Functions.LogInformation(email));
                            return userSalt;
                        }
                        else
                        {
                            MessageBox.Show("Email not found in the database soz");
                            LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                            Constants_Functions.LogInformation(email));
                            return userSalt;
                        }
                    }   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation(email, ex.Message));
                return userSalt;
            }
        }
        public bool CheckUserPassword(string email, string password, byte[] salt)
        {
            // This method checks the users Password matches the email to allow them to login
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Opens connection
                connection.Open();
                // Gets the relevant query
                string passwordQuery = Constants_Functions.PasswordQuery;

                using (SqlCommand sqlCommand = new SqlCommand(passwordQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", email));

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Reads the salt and the hashed password into byte arrays
                            byte[] storedPassword = (byte[])reader["Password"];
                            byte[] hashedPassword = (Constants_Functions.HashPassword(password, salt));

                            // SequenceEqual compares each byte in the array wheras !=
                            // compares only object references
                            if (!storedPassword.SequenceEqual(hashedPassword))
                            {
                                // Not equal so no login
                                MessageBox.Show("Password wrong sorry mate");
                                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                                Constants_Functions.LogInformation(email, "passwords not equal"));
                                return false;
                            }
                            else
                            {
                                // Equal so login
                                MessageBox.Show("I'm in");
                                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                                Constants_Functions.LogInformation(email, "Return true"));
                                return true;
                            }
                        }
                        else
                        {
                            // Reader not able to read password must be wrong
                            MessageBox.Show("Password wrong soz");
                            LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                            Constants_Functions.LogInformation(email, "error"));
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation(email, ex.Message));
                return false;
            }
        }
        public bool IsEmailTaken(string email)
        {
            // This method checks to see if an email is already in use when trying to make an account
            SqlConnection connection = new SqlConnection(connectionString);
            // Loads the relevant query
            string checkEmailQuery = Constants_Functions.CheckEmailQuery;

            try
            {
                // Opens connection
                connection.Open();
                // Runs command and adds the paramaters
                using (SqlCommand sqlCommand = new SqlCommand(checkEmailQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", email));
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                        Constants_Functions.LogInformation(email, reader.HasRows));
                        // Simply returns if the reader managed to read anything or not
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation(email, ex.Message));
                return false;
            }

        }
        public void AddUserToDB(string email, string password)
        {   
            // Adds a user into the DB
            SqlConnection connection = new SqlConnection(connectionString);
            UserRegistrationManager userRegistrationManager = new UserRegistrationManager();
            // Generates a salt for the user using userRegistrationManager
            byte[] userSalt = userRegistrationManager.GenerateSalt(16);
            // Hashes the users password
            byte[] securePassword = Constants_Functions.HashPassword(password, userSalt);

            try
            {
                connection.Open();
                // Gets the relevant query
                string userInsertQuery = Constants_Functions.UserInsertQuery;

                // Adds all the paramaters to the query
                using (SqlCommand sqlCommand = new SqlCommand(userInsertQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@username", email));
                    sqlCommand.Parameters.Add(new SqlParameter("@password", securePassword));
                    sqlCommand.Parameters.Add(new SqlParameter("@userSalt", userSalt));
                    // Thease are booleans but must be stored as a 1 or 0 so use a single bit
                    sqlCommand.Parameters.Add(new SqlParameter("@Is_Admin", SqlDbType.Bit) { Value = false });
                    sqlCommand.Parameters.Add(new SqlParameter("@Is_Db_Observer", SqlDbType.Bit) { Value = false });
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Task failed successfully");
                    LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                    Constants_Functions.LogInformation(email, userSalt));
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation(email, ex.Message));
            }
        }
        public void SaveFile(string filePath)
        {
            // Lets the user save a file into the DB
            using (Stream stream = File.OpenRead(filePath))
            {
                // Reads the file into a byte array
                byte[] fileData = new byte[stream.Length];
                stream.Read(fileData, 0, fileData.Length);

                String fileExtension = Path.GetExtension(filePath);
                String fileName = Path.GetFileName(filePath);
                String FileQuery = Constants_Functions.SaveFileQuery;

                SqlConnection connection = new SqlConnection(connectionString);
                try
                { 
                    // Opens the connection then adds all the paramaters
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(FileQuery, connection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@fileData", fileData));
                        sqlCommand.Parameters.Add(new SqlParameter("@extension", fileExtension));
                        sqlCommand.Parameters.Add(new SqlParameter("@fileName", fileName));
                        sqlCommand.ExecuteNonQuery();
                        // Sets up the string to notify the observers the DB has changed
                        string fileSavedNotification = "A new file has been saved by: " +
                        LoggedInAs.GetInstanceOfLoggedInAs().CurrentUserEmail + " it is named: " +
                        fileName;
                        NotifyDbObservers(fileSavedNotification);
                        LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                        Constants_Functions.LogInformation(filePath));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                    Constants_Functions.LogInformation(ex.Message));
                }
            }
        }
        public void OpenFile(int id)
        {
            // Opens a file for the user to see
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Open the conenction and get the right query ready
                connection.Open();
                string fileOpenQuery = Constants_Functions.fileOpenQuery;

                using (SqlCommand sqlCommand = new SqlCommand(fileOpenQuery, connection))
                {
                    // Add the file id as a paramater
                    sqlCommand.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var name = reader["File_Name"].ToString();
                            var data = (byte[])reader["File_Data"];
                            var extn = reader["File_Extension"].ToString();
                            var newFileName = name.Replace(extn, DateTime.Now.ToString("ddMMyyyyhhmmss")) + extn;

                            // Get the file and open it
                            File.WriteAllBytes(newFileName, data);
                            System.Diagnostics.Process.Start(newFileName);

                            LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                            Constants_Functions.LogInformation(id));
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation(ex.Message));
            }
        }
        public DataTable DisplayFileData()
        {
            // Loads the file data saved into a data grid to view
            string fileDataQuery = Constants_Functions.FileDataQuery;

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Opens the connection runs the query and returns the data table to be loaded into the 
                // data grid view
                connection.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(fileDataQuery, connection))
                {
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                    Constants_Functions.LogInformation());
                    return dt;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation(ex.Message));
                return null;
            }
        }
        public void SaveLogFile(string filePath, string email)
        {
            // Method is not acually used in the program but allows the user to save log information into
            // a DB which for a larger organisation could make error management easier
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
                        LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                        Constants_Functions.LogInformation(filePath, email));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                    Constants_Functions.LogInformation(ex.Message));
                }
            }
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
        //method to add paramaters from updateDB Form in DBExcludingUsers
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
            //Get an empty dataset with the structure of the software table
            DataSet schema = DbConnector.GetInstanceOfDBConnector().getDataSet("SELECT TOP 0 * FROM Software");

            //Extract column names from dataset schema
            return schema.Tables[0].Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
        }

        public DataSet SearchAndFiltering(string searchText)
        {
            //Construct base SQL query
            string query = "SELECT * FROM Software WHERE ";

            //GetColumnNames method to retrieve column names
            string[] columnNames = GetColumnNames();

            //Append conditions for each column in Where clause
            for (int i = 0; i < columnNames.Length; i++)
            {
                if (i > 0)
                {
                    query += " OR ";
                }
                query += $"CONVERT(NVARCHAR(MAX), [{columnNames[i]}]) LIKE @searchText";
            }

            //Create param for searchtext
            SqlParameter parameter = new SqlParameter("@searchText", searchText);


            try
            {
                //Initialize dataset to store filtered data
                DataSet filteredData = new DataSet();
                try
                {
                    //Connect to database w sqlconnection
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        //sqlcmd execute query
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            //Add searchtxt param to cmd
                            command.Parameters.Add(new SqlParameter("@searchText", searchText));

                            //Use sqldataadapter to fill datatable w results
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                //Fill datatable with the results
                                adapter.Fill(filteredData);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Handle exceptions
                    MessageBox.Show(ex.Message);
                    return null;
                }

                if (filteredData != null && filteredData.Tables.Count > 0)
                {
                    //Return filtered data
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
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}

