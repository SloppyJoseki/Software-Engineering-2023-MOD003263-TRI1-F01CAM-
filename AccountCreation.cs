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
    public partial class AccountCreation : Form
    {
        public AccountCreation()
        {
            InitializeComponent();
            CreateButton.Enabled = false;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }

        private void AccountCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ensure that the form is being closed by the user (not programmatically).
                Application.Exit();
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            UserRegistrationManager userRegistrationManager = new UserRegistrationManager();
            string regCode = userRegistrationManager.GenerateRegistrationCode();
            if (userRegistrationManager.SendRegistrationCodeEmail(EmailBox.Text, regCode))
            {
                this.Hide();
                new RegistrationCodeForm(regCode, EmailBox.Text, PasswordBox.Text).Show();
            }        
        }

        private void EmailBox_TextChanged(object sender, EventArgs e)
        {
            CreateButtonControl();
        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            CreateButtonControl();
        }

        public void CreateButtonControl()
        {
            if (!string.IsNullOrEmpty(EmailBox.Text) && !string.IsNullOrEmpty(PasswordBox.Text))
            {
                CreateButton.Enabled = true;
            }
            else 
            {
                CreateButton.Enabled = false;
            }
        }
    }
}
