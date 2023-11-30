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
using LoginInterface;
using System.Collections;

namespace SearchFeature
{
    public partial class SearchForm : Form
    {


        public SearchForm()
        {
            InitializeComponent();
        }


        private void FillGrid()
        {
            //Retrieves data from the Software table
            DataSet dataS = DbConnector.GetInstanceOfDBConnector().getDataSet("SELECT * FROM Software");

            //Check if dataset has data
            if (dataS != null && dataS.Tables.Count > 0)
            {
                //Set the Datagrid's datasource to datatale in dataset its a dataparteyyyy
                dataGridView1.DataSource = dataS.Tables[0];
            }
            else
            {
                //No data found
                Console.WriteLine("No data found in software table");
            }

        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

            //Get the search text from the searchbar 
            string searchText = searchBar.Text;

            //Use search&filtering method to filter data based on text people put in (searchtext)
            DataSet dtSt = DbConnector.GetInstanceOfDBConnector().SearchAndFiltering(searchText);

            dataGridView1.DataSource = dtSt.Tables[0];
        }
        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //comboBoxType, won't let me change
        {
            //Copy of comboCloud below cuz they have same function, no need 4 comment
            string selectedType = comboBoxType.SelectedItem?.ToString();
            string query1 = "SELECT * FROM Software WHERE Software_Type = '" + selectedType + "'";
            DataSet dtStt = DbConnector.GetInstanceOfDBConnector().getDataSet(query1);
            if (dtStt != null && dtStt.Tables.Count > 0)
            {
                dataGridView1.DataSource = dtStt.Tables[0];
            }
            else
            {
                Console.WriteLine("No data found");
            }

        }


        private void SearchForm_Load_1(object sender, EventArgs e)
        {
            // Fill Datagrid with initial data at beginning
            FillGrid();
        }

        private void comboBoxCloud_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get selected value from combobox
            string selectedCloud = comboBoxCloud.SelectedItem?.ToString();

            //Create a query to filter data based on selCloud
            string query1 = "SELECT * FROM Software WHERE Cloud = '" + selectedCloud + "'";

            //Execute query get dataset
            DataSet dtStt = DbConnector.GetInstanceOfDBConnector().getDataSet(query1);

            //checks if dataset is not null and has tables
            if (dtStt != null && dtStt.Tables.Count > 0)
            {
                //update grid with juicy filtered data
                dataGridView1.DataSource = dtStt.Tables[0];
            }
            else
            {
                //handle case where no data is found
                Console.WriteLine("No data foundy");
            }
        }

        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ensure that the form is being closed by the user (not programmatically).
                Application.Exit();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            //Fills grid with fresh data
            FillGrid();

            //Reset the filter criteria
            searchBar.Text = "";
            comboBoxType.SelectedIndex = -1;
            comboBoxCloud.SelectedIndex = -1;
        }

        public static DataGridViewRow selectedrow;

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedrow = dataGridView1.Rows[e.RowIndex];
                Form2.getform2.ShowDialog();
            }
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedrow = dataGridView1.Rows[e.RowIndex];
                Form2.getform2.ShowDialog();
            }
        }
    }
}
