using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            DGV.DataSource = dataTable;
            //enables editing
            DGV.ReadOnly = false;
            //auto generate the columns
            DGV.AutoGenerateColumns = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DbConnector dbconn = DbConnector.GetInstanceOfDBConnector();
            DataSet dscompany = dbconn.getDataSet("SELECT * From Company");
            DGV.DataSource = dscompany.Tables[0];
    
        }

        private void Button7_Click(object sender, EventArgs e)
        { 
            new Dashboard().Show();
            this.Hide();
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ensure that the form is being closed by the user (not programmatically).
                Application.Exit();
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBExcludingUsersDataSet.Company' table. You can move, or remove it, as needed.
            this.companyTableAdapter.Fill(this.dBExcludingUsersDataSet.Company);

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGV.DataSource = dataTable;
            //enables editing
            DGV.ReadOnly = false;
            //auto generate the columns
            DGV.AutoGenerateColumns = true;


            //sends user to update form so they can update the row they selected
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {

                    // Create a new form or use the data retrieved to open a new form
                    // For example, open a new form with the selected data
                    updateDB form_update = new updateDB();
                form_update.Show();
                this.Hide(); // Close the UpdateForm after updating

            }
        }
    }
}
