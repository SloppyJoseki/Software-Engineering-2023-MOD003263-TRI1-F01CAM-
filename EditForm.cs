using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LoginInterface
{
    public partial class EditForm : Form
    {
        DBExcludingUsersDataSet dataTable = new DBExcludingUsersDataSet();

        public EditForm()
        {
            InitializeComponent();

            //binding to the database
            dataGridView1.DataSource = dataTable;
            //enables editing
            dataGridView1.ReadOnly = false;
            //auto generate the columns
            dataGridView1.AutoGenerateColumns = true;

        }


        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //sends user back to the dashbord
            Dashboard dashbord = new Dashboard();
            dashbord.Show();

            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //closes window
            this.Close();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //sends users to vendor search
        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dBExcludingUsersDataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
