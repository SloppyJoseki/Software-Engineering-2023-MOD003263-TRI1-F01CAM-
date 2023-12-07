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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // Disables the login button
            LoginButton.Enabled = false;
        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            // Checks for text in the Password box
            Constants_Functions.ButtonControl(PasswordBox.Text, UsernameBox.Text, LoginButton);
        }

        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {
            // Checks for text in the Email box
            Constants_Functions.ButtonControl(PasswordBox.Text, UsernameBox.Text, LoginButton);
        }

        private void CreateAccount_Click(object sender, EventArgs e)
        {
            // Opens a new AccountCreation form
            LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
            Constants_Functions.LogInformation());
            this.Hide();
            new AccountCreationForm().Show();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Checks if the user is able to login 
           byte[] userSalt = DbConnector.GetInstanceOfDBConnector().CheckEmailGetSalt(UsernameBox.Text);
           bool isLogin = DbConnector.
           GetInstanceOfDBConnector().CheckUserPassword(UsernameBox.Text, PasswordBox.Text, userSalt);
           // If they can then:
           if (isLogin)
           {
                // Updates who is logged in
                LoggedInAs.GetInstanceOfLoggedInAs().CurrentUserEmail = UsernameBox.Text;
                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation( "UsernameBox.Text: " + UsernameBox.Text));
                // Opens a new Dashboard form
                this.Hide();
                new Dashboard().Show();
           }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Instantly create the list of Db observers on launch
            DbConnector.GetInstanceOfDBConnector().GetObserverList();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ensure that the form is being closed by the user (not programmatically).
                Application.Exit();
            }
        }
    }  
}
