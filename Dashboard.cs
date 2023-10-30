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

            this.Close();
        }
    }
}
