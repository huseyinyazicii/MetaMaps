using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host;
        private int _port;
        private bool _enableSSL;
        private string _userName;
        private string _password;

        public SmtpEmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            _password = password;
            _userName = userName;
            _enableSSL = enableSSL;
            _port = port;
            _host = host;
        }

        public Task SendEmail(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSSL
            };
            return client.SendMailAsync(new MailMessage(_userName, email, subject, htmlMessage)
            {
                IsBodyHtml = true
            });
        }
    }
}
