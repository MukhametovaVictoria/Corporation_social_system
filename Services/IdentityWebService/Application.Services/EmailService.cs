using Application.Interfaces;
using Infrastructure.Helpers;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Text;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Email sending
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="subject">Subject</param>
        /// <param name="message">Content</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var encryptedUsername = _configuration["EmailSettings:Username"];
                var encryptedPassword = _configuration["EmailSettings:Password"];
                var domain = _configuration["EmailSettings:Domain"];
                var sender = _configuration["EmailSettings:Email"];
                var portStr = _configuration["EmailSettings:Port"];
                int.TryParse(portStr, out var port);
                if (port == 0)
                    port = 587;

                if(encryptedPassword == null)
                {
                    _logger.LogError("Error! Email password is empty.");
                    return;
                }
                if (encryptedUsername == null)
                {
                    _logger.LogError("Error! Email login is empty.");
                    return;
                }

                var username = EncryptionHelper.Decrypt(encryptedUsername);
                var password = EncryptionHelper.Decrypt(encryptedPassword);

                MimeMessage mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("Some_name", sender)); //отправитель сообщения
                mimeMessage.To.Add(new MailboxAddress(Encoding.UTF8, email, email)); //адресат сообщения
                mimeMessage.Subject = subject; //тема сообщения
                mimeMessage.Body = new BodyBuilder() { HtmlBody = message }.ToMessageBody(); //тело сообщения (так же в формате HTML)

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(domain, port, SecureSocketOptions.StartTls); //либо использум порт 465
                    await client.AuthenticateAsync(username, password); //логин-пароль от аккаунта
                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex) 
            {
#pragma warning disable S6667 // Logging in a catch clause should pass the caught exception as a parameter.
                _logger.LogError("An error occured while preparing|sending email: {Message}.", ex.Message);
#pragma warning restore S6667 // Logging in a catch clause should pass the caught exception as a parameter.
            }
        }
    }
}