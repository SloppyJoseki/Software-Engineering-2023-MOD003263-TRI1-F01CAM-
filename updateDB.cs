using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace LoginInterface
{
    public partial class updateDB : Form
    {
        public updateDB()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                DbConnector dbconn = DbConnector.GetInstanceOfDBConnector();
                //retrieve data from text boxes
                try
                {                
                    //ensures the new row's primary key has a value
                    int data0 = Convert.ToInt32(textBox0.Text);
                    string data1 = textBox1.Text;
                    string data2 = textBox2.Text;
                    int data3 = Convert.ToInt32(textBox3.Text);
                    int data4 = Convert.ToInt32(textBox4.Text);
                    string data5 = textBox5.Text;
                    dbconn.addToDB("INSERT INTO Company"+
                                    "(Company_ID, Company_name, Company_website, Company_established, "+
                                    "   No_of_Employees, Internal_Professional_Services) "+ 
                                    "   VALUES (@Company_ID, @Company_name, @Company_website, @Company_established, "+
                                    "   @No_of_Employees, @Internal_Professional_Services)"
                                    , data0, data1, data2, data3, data4, data5);    
                }
                catch (FormatException)
                {
                    //handles FormatException when the text is not in a valid integer
                    MessageBox.Show("Please enter valid integer values in Table_ID attribute3 and attribute4.");
                }
                catch (OverflowException)
                {
                    //handles OverflowException when an entered value is too large
                    MessageBox.Show("Entered values in attribute3 or attribute4 are too large.");
                }
                catch (Exception ex)
                {
                    //handles other exceptions
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                                      
                //create an instance of DisplayForm
                EditForm displayForm = new EditForm();
                //show the DisplayForm
                displayForm.Show();
                //closes the UpdateForm after updating
                this.Hide(); 

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //create an instance of DisplayForm
            EditForm displayForm = new EditForm();
            //show the DisplayForm
            displayForm.Show();
            //close the UpdateForm after updating
            this.Hide();
        }

        private void update_Click(object sender, EventArgs e)
        {
            DbConnector dbconn = DbConnector.GetInstanceOfDBConnector();
            //retrieve data from text boxes
            try
            {
                //ensures the new row's primary key has a value
                int data0 = Convert.ToInt32(textBox0.Text);
                string data1 = textBox1.Text;
                string data2 = textBox2.Text;
                int data3 = Convert.ToInt32(textBox3.Text);
                int data4 = Convert.ToInt32(textBox4.Text);
                string data5 = textBox5.Text;
                //sets up sql query to change data in specified row
                dbconn.updateToDB("UPDATE Company " +
                                       "SET Company_name = @CompanyName, " +
                                       "    Company_website = @CompanyWebsite, " +
                                       "    Company_established = @CompanyEstablished, " +
                                       "    No_of_Employees = @NumberOfEmployees, " +
                                       "    Internal_Professional_Services = @InternalServices " +
                                       "WHERE Company_ID = @CompanyID", data0, data1, data2, data3, data4, data5);

            }
            catch (FormatException)
            {
                //handles FormatException when the text is not in a valid integer
                MessageBox.Show("Please enter valid integer values in Table_ID attribute3 and attribute4.");
            }
            catch (OverflowException)
            {
                //handles OverflowException when an entered value is too large
                MessageBox.Show("Entered values in attribute3 or attribute4 are too large.");
            }
            catch (Exception ex)
            {
                //handles other exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }           
            
             //create an instance of DisplayForm
             EditForm displayForm = new EditForm();
             //show the DisplayForm
             displayForm.Show();
             //close the UpdateForm after updating
             this.Hide(); 
        
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
