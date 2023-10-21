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
    public partial class RegistrationCodeForm : Form
    {
        string regCode;
        public RegistrationCodeForm(string regCode)
        {
            InitializeComponent();
            this.regCode = regCode;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AccountCreation().Show();
        }

        private void RegistrationCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ensure that the form is being closed by the user (not programmatically).
                Application.Exit();
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string userCode = RegCodeBox.Text;

            if (userCode != regCode)
            {
                MessageBox.Show("Fucking Moron");
            }
            else if (userCode == regCode)
            {
                MessageBox.Show("Well done");
            }
        }

        private void RegCodeBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
