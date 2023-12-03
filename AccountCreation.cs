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
            // Setting the Create Account button to not be clickable
            CreateButton.Enabled = false;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // Close this form and return to the Login screen
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
            // Use the userRegistrationManager to generate the random 6 digit code and send the email with it
            UserRegistrationManager userRegistrationManager = new UserRegistrationManager();
            string regCode = userRegistrationManager.GenerateRegistrationCode();
            if (userRegistrationManager.SendRegistrationCodeEmail(EmailBox.Text, regCode))
            {
                this.Hide();
                // Pass the user info into the next form
                new RegistrationCodeForm(regCode, EmailBox.Text, PasswordBox.Text).Show();
            }        
        }

        private void EmailBox_TextChanged(object sender, EventArgs e)
        {
            // Check for text in the Email box
            CreateButtonControl();
        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            // Check for text in the Password box
            CreateButtonControl();
        }

        public void CreateButtonControl()
        {
            // If there is text in both the Email and Password textboxes then the Create account button will
            // become clickable
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
