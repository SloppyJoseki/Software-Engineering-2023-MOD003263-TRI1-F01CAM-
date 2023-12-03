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
        readonly string regCode;
        readonly string email;
        readonly string password;
        public RegistrationCodeForm(string regCode, string email, string password)
        {
            // On creation initialize the attributes with the paramaters passed in
            InitializeComponent();
            this.regCode = regCode;
            this.email = email;
            this.password = password;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // On click will take you back to the AccountCreation form
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
            // Check if the registration code the user enters is correct
            string userCode = RegCodeBox.Text;

            if (userCode != regCode)
            {
                MessageBox.Show("Please try again");
                return;
            }

            // If its correct enter the users information into the database so the account can be used
            DbConnector.GetInstanceOfDBConnector().AddUserToDB(email, password);
            this.Hide();
            new LoginForm().Show();
        }
    }
}
