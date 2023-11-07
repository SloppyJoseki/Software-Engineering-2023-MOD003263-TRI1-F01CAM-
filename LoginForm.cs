﻿using System;
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
            LoginButton.Enabled = false;
        }

        private void PasswordLabel_Click(object sender, EventArgs e)
        {

        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            LoginButtonControl();
        }

        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {
            LoginButtonControl();
        }

        private void CreateAccount_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AccountCreation().Show();
        }

        private void EmailLabel_Click(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
           byte[] userSalt = DbConnector.GetInstanceOfDBConnector().CheckEmailGetSalt(UsernameBox.Text);
           bool isLogin = DbConnector.GetInstanceOfDBConnector().CheckUserPassword(UsernameBox.Text, PasswordBox.Text, userSalt);
           if (isLogin)
            {
                this.Hide();
                new Dashboard().Show();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ensure that the form is being closed by the user (not programmatically).
                Application.Exit();
            }
        }

        public void LoginButtonControl()
        {
            if (!string.IsNullOrEmpty(UsernameBox.Text) && !string.IsNullOrEmpty(PasswordBox.Text))
            {
                LoginButton.Enabled = true;
            }
            else
            {
                LoginButton.Enabled = false;
            }
        }
    }  
}
