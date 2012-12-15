using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace muagicungban
{
    public static class Extension
    {
        public static string md5(this string input)
        {
            return Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(input)));
        }

        public static void SendEmail(string address, string subject, string message)
        {
            string email = "muagicungban.vn@gmail.com";
            string password = "@%@%!#@%";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }

        public static string AutoString(int length)
        {
            int intZero = '0';
            int intNine = '9';
            int intA = 'A';
            int intZ = 'Z';
            int intCount = 0;
            int intRandomNumber = 0;
            string result = "";
            Random random = new Random(System.DateTime.Now.Millisecond);
            while (intCount < length)
            {
                intRandomNumber = random.Next(intZero, intZ);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine) || (intRandomNumber >= intA) && (intRandomNumber <= intZ)))
                {
                    result = result + (char)intRandomNumber;
                    intCount = intCount + 1;
                }
            }
            return result;
        }
    }
}