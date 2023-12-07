using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginInterface
{
    public partial class FilesForm : Form
    {
        public FilesForm()
        {
            InitializeComponent();
        }

        private void FilesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ensure that the form is being closed by the user (not programmatically).
                Application.Exit();
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            // Lets the user look throug any files they have to fill the text box with a file path
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            FilePathTextBox.Text = ofd.FileName;
        }
         
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Saves any file the user wants so long as the text box contains the file path
            DbConnector.GetInstanceOfDBConnector().SaveFile(FilePathTextBox.Text);
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            // Fills the data grid with all file information in the DB
            DataTable dataTable = DbConnector.GetInstanceOfDBConnector().DisplayFileData();
            DocumentViewer.DataSource = dataTable;
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            // Lets the user open a file so long as they select the row its in
            var selectedRow = DocumentViewer.SelectedRows;
            foreach (var row in selectedRow)
            {
                int id = (int)((DataGridViewRow)row).Cells[0].Value;
                DbConnector.GetInstanceOfDBConnector().OpenFile(id);
            }
        }

        private void Home_Click(object sender, EventArgs e)
        {
            // Takes the user back to the dashboard
            new Dashboard().Show();
            this.Hide();
        }
    }
}
