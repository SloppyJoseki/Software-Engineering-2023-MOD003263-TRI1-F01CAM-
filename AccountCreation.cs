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
        }

        private void EmailLabel_Click(object sender, EventArgs e)
        {

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
            userRegistrationManager.SendRegistrationCodeEmail("hg419@student.aru.ac.uk", regCode);
            this.Hide();
            new RegistrationCodeForm(regCode).Show();

        }

        private void AccountCreation_Load(object sender, EventArgs e)
        {

        }
    }
}
