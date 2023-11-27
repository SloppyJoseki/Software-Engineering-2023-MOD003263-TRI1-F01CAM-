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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void EditDbButton_Click(object sender, EventArgs e)
        {
            EditForm form_edit = new EditForm();

            form_edit.Show();
            this.Hide();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ensure that the form is being closed by the user (not programmatically).
                Application.Exit();
            }
        }

        private void ViewFilesButton_Click(object sender, EventArgs e)
        {
            new FilesForm().Show();
            this.Hide();
        }
    }
}
