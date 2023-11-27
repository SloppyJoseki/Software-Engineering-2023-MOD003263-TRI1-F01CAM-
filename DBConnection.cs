using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchFeature
{
    class DBConnection
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

        public DataSet getDataSet(string sqlQuery, params SqlParameter[] parameters)
        {
            DataSet dataset = new DataSet();

            try
            {
                using(SqlConnection connToDB = new SqlConnection(dBConnectionString))
                {
                    connToDB.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connToDB))
                    {
                        if (parameters != null)
                        {
                            adapter.SelectCommand.Parameters.AddRange(parameters);
                        }

                        adapter.Fill(dataset);
                    }

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
