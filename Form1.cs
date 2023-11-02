using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Projectver1
{
    public partial class Form1 : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Daria\Projectver1\Projectver1\SoftwareBits.mdf;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        public void fillGrid()
        {
            Con.Open();

            string query = "SELECT * FROM Software_tbl";
            SqlDataAdapter daAdapt = new SqlDataAdapter(query, Con);
            SqlCommandBuilder sComBuild = new SqlCommandBuilder(daAdapt);
            var dataS = new DataSet();
            daAdapt.Fill(dataS);
            dataGV.DataSource = dataS.Tables[0];

            Con.Close();
        }

        public void textBoxFilter()
        {
            Con.Open();

            string query = "SELECT * FROM Software_tbl where SftName = '"+typeBox.Text+"'";
            SqlDataAdapter daAdapt = new SqlDataAdapter(query, Con);
            SqlCommandBuilder sComBuild = new SqlCommandBuilder(daAdapt);
            var dataS = new DataSet();
            daAdapt.Fill(dataS);
            dataGV.DataSource = dataS.Tables[0];

            Con.Close();
        }

        public void typeSoftwareFilter()
        {
            Con.Open();

            string query = "SELECT * FROM Software_tbl where SftType = '" + typeSoftware.SelectedItem.ToString() + "'";
            SqlDataAdapter daAdapt = new SqlDataAdapter(query, Con);
            SqlCommandBuilder sComBuild = new SqlCommandBuilder(daAdapt);
            var dataS = new DataSet();
            daAdapt.Fill(dataS);
            dataGV.DataSource = dataS.Tables[0];

            Con.Close();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            typeSoftwareFilter();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            textBoxFilter();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            fillGrid();
            typeBox.Text = "";
        }
    }
}
