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
    internal class UserRegistrationManager
    {
        public string GenerateRegistrationCode()
        {
            Random generator = new Random();
            String registrationCode = generator.Next(0, 1000000).ToString("D6");
            return registrationCode;
        }

        public void SendRegistrationCodeEmail(string targetEmail, string authCode)
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
                    mail.Body = "Hello your authentication code is: " + authCode;

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
        public void RegisterUser(string username, string password)
        {
            string authCode = GenerateRegistrationCode();
            SendRegistrationCodeEmail(username, authCode);

        }
    }
}
