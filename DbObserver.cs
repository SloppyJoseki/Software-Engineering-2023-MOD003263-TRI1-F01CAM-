using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginInterface
{
    internal class DbObserver : IDbObserver
    {
        readonly private string userEmail;
        public DbObserver(string email)
        {
            userEmail = email;
        }

        internal IDbObserver IDbObserver
        {
            get => default;
            set
            {
            }
        }

        public void Update(string message)
        {
            // Will send an email to the user informing them of a db update

            string senderEmail = "ThisIsForMyUniProject@gmail.com";

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(senderEmail, Properties.Settings.Default.EmailAppPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mail = new MailMessage())
                {
                    try
                    {
                        mail.From = new MailAddress(senderEmail);
                        mail.To.Add(userEmail);
                        mail.Subject = "Someone has updated the Database";
                        mail.Body = message;
                        smtpClient.Send(mail);

                        LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                        Constants_Functions.LogInformation(message));
                    }
                    catch
                    {
                        MessageBox.Show("Error updating user: " + userEmail);
                        LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                        Constants_Functions.LogInformation());
                    }
                }
            }
        }
    }
}
