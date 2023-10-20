using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace LoginInterface
{
    class DbConnector
    {
        private static DbConnector _instance;

        private String connectionString;

        private SqlConnection connectToDb;

        private DbConnector()
        {
            connectionString = Properties.Settings.Default.DBConnectionString;
        }

        public static DbConnector GetInstanceOfDBConnector()
        {
            if (_instance == null)
            {
                _instance = new DbConnector();
                return _instance;
            }
            return _instance;
        }

        public void CheckEmail(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                
                connection.Open();

                String emailQuery = "SELECT 1 FROM UserData WHERE Email COLLATE Latin1_General_CS_AS = @Email";

                using (SqlCommand sqlCommand = new SqlCommand(emailQuery, connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", email));

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows == true)
                        {
                            MessageBox.Show("Found");
                        }
                        else
                        {
                            MessageBox.Show("Failed");
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                
            }
        }

        public void SendEmail(string targetEmail)
        {
            string senderEmail = "ThisIsForMyUniProject@gmail.com";

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(senderEmail, Properties.Settings.Default.EmailAppPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(targetEmail);
                    mail.Subject = "Testing123";
                    mail.Body = "Hello World!";

                    try
                    {
                        smtpClient.Send(mail);
                        MessageBox.Show("Sent bitch");
                    }
                    catch
                    {
                        MessageBox.Show($"Failed to send email: {senderEmail}");
                    }

                }
            }
        }
    }
}

