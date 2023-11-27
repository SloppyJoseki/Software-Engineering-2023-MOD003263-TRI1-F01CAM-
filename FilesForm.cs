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

        private void FilesForm_Load(object sender, EventArgs e)
        {

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
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            FilePathTextBox.Text = ofd.FileName;
        }
         
        private void SaveButton_Click(object sender, EventArgs e)
        {
            DbConnector.GetInstanceOfDBConnector().SaveFile(FilePathTextBox.Text);
            MessageBox.Show("Saved");
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = DbConnector.GetInstanceOfDBConnector().DisplayFileData();
            DocumentViewer.DataSource = dataTable;
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var selectedRow = DocumentViewer.SelectedRows;
            foreach (var row in selectedRow)
            {
                int id = (int)((DataGridViewRow)row).Cells[0].Value;
                DbConnector.GetInstanceOfDBConnector().OpenFile(id);
            }
        }

        private void Home_Click(object sender, EventArgs e)
        {
            new Dashboard().Show();
            this.Hide();
        }
    }
}
