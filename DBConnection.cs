using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchyTesty
{
    internal class DBConnection
    {
        private static DBConnection _instance;

        private string dBConnectionString;

        private DBConnection()
        {
            dBConnectionString = Properties.Settings.Default.DBConnectionString;
        }

        //methods

        public static DBConnection getInstanceOfDBConnection()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public DataSet getDataSet(string sqlQuery)
        {
            DataSet dataset = new DataSet();

            try
            {
                using (SqlConnection connToDB = new SqlConnection(dBConnectionString))
                {
                    connToDB.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connToDB);

                    adapter.Fill(dataset);
                }

            }

            catch (SqlException ex)
            {
                MessageBox.Show($"Database Error:" + ex.Message);
            }

            return dataset;
        }
    }
}
