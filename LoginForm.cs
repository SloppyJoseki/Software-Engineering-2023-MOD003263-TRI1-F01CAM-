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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string testEmail = "harygiddens@gmail.com";
            DbConnector dbConnector = DbConnector.GetInstanceOfDBConnector();
            dbConnector.CheckEmail(testEmail);
            dbConnector.SendEmail("hg419@student.aru.ac.uk");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }  
}
