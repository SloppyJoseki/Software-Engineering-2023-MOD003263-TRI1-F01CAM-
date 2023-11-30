using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace LoginInterface
{
    internal class UserRegistrationManager
    {
        public LoginForm LoginForm
        {
            get => default;
            set
            {
            }
        }

        public string GenerateRegistrationCode()
        {
            Random generator = new Random();
            String registrationCode = generator.Next(0, 1000000).ToString("D6");

            LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
            Constants_Functions.LogInformation(registrationCode));
            return registrationCode;
        }

        public bool SendRegistrationCodeEmail(string targetEmail, string authCode)
        {
            if (DbConnector.GetInstanceOfDBConnector().IsEmailTaken(targetEmail))
            {
                MessageBox.Show("Sorry that email is already in use");

                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation(targetEmail, authCode));
                return false;
            }
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
                        mail.To.Add(targetEmail);
                        mail.Subject = "Testing123";
                        mail.Body = "Hello your authentication code is: " + authCode;
                        smtpClient.Send(mail);
                        MessageBox.Show("Sent");

                        LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                        Constants_Functions.LogInformation(senderEmail, authCode, "email sent"));
                        return true;
                    }
                    catch
                    {
                        MessageBox.Show("Failed to send email please enter a valid email address");

                        LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                        Constants_Functions.LogInformation(senderEmail, authCode, "email not sent"));
                        return false;
                    }
                }
            }
        }

        public byte[] GenerateSalt(int length)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[length];
                rng.GetBytes(salt);

                LoggerHelper.Log(Constants_Functions.LogEndpoint.File,
                Constants_Functions.LogInformation(salt));
                return salt;
            }
        }

    }
}
