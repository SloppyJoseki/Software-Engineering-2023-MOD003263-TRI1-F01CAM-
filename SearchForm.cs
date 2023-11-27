using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Odbc;
using System.Reflection;

namespace SearchFeature
{
    public partial class SearchForm : Form
    {
        private DBConnection dbConn;

        public SearchForm()
        {
            InitializeComponent();
            dbConn = DBConnection.getInstanceOfDBConnection();
        }


        private void SearchAndFilter(string searchText, string typeFilter, string cloudFilter)
        {
            // Construct base SQL query
            string query = "SELECT * FROM Software WHERE ";

            // Implement GetColumnNames method 
            string[] columnNames = GetColumnNames(); 
            for (int i = 0; i < columnNames.Length; i++)
            {
                if (i > 0)
                {
                    query += " OR ";
                }
                query += $"CONVERT(NVARCHAR(MAX), [{columnNames[i]}]) LIKE @searchText";

            }

            //Add filters based on searchbar and combobox
            //searchtext
            if (!string.IsNullOrEmpty(searchText))
            {
                query += "CONVERT(NVARCHAR(MAX), [Software_name]) LIKE @searchText";
            }

            //typefilter
            if (!string.IsNullOrEmpty(typeFilter))
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " AND ";

                }
                query += "[Software_Type] = @typeFilter";
            }

            //cloudfilter
            if (!string.IsNullOrEmpty(cloudFilter))
            {
                if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrEmpty(typeFilter))
                {
                    query += " AND ";

                }
                query += "[Cloud] = @cloudFilter";
            }


            //Add parameters to query
            SqlParameter[] parameters =
            {
                new SqlParameter("@SearchText", $"%{searchText}%"),
                new SqlParameter("@typeFilter", typeFilter),
                new SqlParameter("@cloudFilter", cloudFilter)

            };


            DataSet filteredData = dbConn.getDataSet(query, parameters);

            if (filteredData != null && filteredData.Tables.Count > 0)
            {
                dataGridView1.DataSource = filteredData.Tables[0];
            }

            else

            {
                Console.WriteLine($"No matching data found for search: {searchText}, Type: {typeFilter}, Cloud: {cloudFilter}");
            }

        }


        private void FillGrid() 
        {
            DataSet dataS = dbConn.getDataSet("SELECT * FROM Software");
            if (dataS != null && dataS.Tables.Count > 0)
            {
                dataGridView1.DataSource = dataS.Tables[0];
            }
            else
            {
                Console.WriteLine("No data found in software table");
            }
        
        }


        private string[] GetColumnNames()
        {
            DataSet schema = dbConn.getDataSet("SELECT TOP 0 * FROM Software");
            return schema.Tables[0].Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        private void buttonSearch_Click(object sender, EventArgs e)
        {

            string searchText = searchBar.Text.Trim();
            string typeFilter = comboBoxType.SelectedItem?.ToString();
            string cloudFilter = comboBoxCloud.SelectedItem?.ToString();
            
            SearchAndFilter(searchText, typeFilter, cloudFilter);
           
        }

        private void searchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //comboBoxType, won't let me change
        {
            //Starts filtering when selected item in comboboxType changes
            buttonSearch_Click(sender, e);
        }

        private void SearchForm_Load_1(object sender, EventArgs e)
        {
            // Fill Datagrid with initial data at beginning
            FillGrid();
        }

        private void comboBoxCloud_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Starts filtering when selected item in comboboxCloud changes
            buttonSearch_Click(sender, e);
        }
    }
}
