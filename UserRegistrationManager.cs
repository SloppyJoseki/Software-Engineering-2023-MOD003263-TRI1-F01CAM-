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
                    try
                    { 
                        mail.From = new MailAddress(senderEmail);
                        mail.To.Add(targetEmail);
                        mail.Subject = "Testing123";
                        mail.Body = "Hello your authentication code is: " + authCode;
                        smtpClient.Send(mail);
                        MessageBox.Show("Sent");
                    }
                    catch
                    {
                        MessageBox.Show("Failed to send email please enter a valid email address");
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
                return salt;
            }
        }
        public byte[] HashPassword(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000))
            {
                return pbkdf2.GetBytes(32);
            }
        }
    }
}
