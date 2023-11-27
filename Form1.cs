using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchyTesty
{
    public partial class Form1 : Form
    {

        private DBConnection dbConn;
        public Form1()
        {
            InitializeComponent();

            dbConn = DBConnection.getInstanceOfDBConnection();
        }


        public void fillGrid()
        {
            try
            {
                DataSet dataS = dbConn.getDataSet("SELECT * FROM Table");
                dataGridView1.DataSource = dataS.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving data:" + ex.Message);
            }

        }


        public void textBoxFilter()
        {
            string searchText = searchBar.Text.Trim();
            string query = $"SELECT * FROM Table WHERE Software_Name = 'searchText'";
            DataSet dataS = dbConn.getDataSet(query);
            dataGridView1.DataSource = dataS.Tables[0];

        }


        public void comboSoftwareFilter()
        {
            string selectedType = comboBox1.SelectedItem?.ToString();
            if (selectedType != null)
            {
                string query = $"SELECT * FROM Table WHERE Cloud = 'selectedType'";
                DataSet dataS = dbConn.getDataSet(query);
                dataGridView1.DataSource = dataS.Tables[0];
            }

        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            textBoxFilter();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSoftwareFilter();
        }

        private void searchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fillGrid();
        }
    }
}
