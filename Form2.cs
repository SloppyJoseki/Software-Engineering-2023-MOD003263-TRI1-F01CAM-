using SearchFeature;
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
    public partial class Form2 : Form
    {
        public static Form2 frm;
        public static Form2 getform2
        {
            get
            {
                if (frm == null)
                {
                    frm = new Form2();
                }

                return frm;
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBoxSN.Text = SearchForm.selectedrow.Cells[0].Value.ToString();
            textBoxD.Text = SearchForm.selectedrow.Cells[1].Value.ToString();
            textBoxAI.Text = SearchForm.selectedrow.Cells[2].Value.ToString();
        }
    }
}
