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
            InitializeComponent();
            this.regCode = regCode;
            this.email = email;
            this.password = password;
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
                MessageBox.Show("Please try again");
                return;
            }

            DbConnector.GetInstanceOfDBConnector().AddUserToDB(email, password);
            this.Hide();
            new LoginForm().Show();
        }
    }
}
