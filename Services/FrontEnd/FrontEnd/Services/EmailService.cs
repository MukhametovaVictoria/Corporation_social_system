using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FrontEnd.Services
{
    public class EmailService
    {
         //System.Net.Mail.SmtpClient
        public void SendEmailDefault()
        {
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.IsBodyHtml = true; //тело сообщения в формате HTML
                message.From = new MailAddress("admin@mycompany.com", "Моя компания"); //отправитель сообщения
                message.To.Add("mail@yandex.ru"); //адресат сообщения
                message.Subject = "Сообщение от System.Net.Mail"; //тема сообщения
                message.Body = "<div style=\"color: red;\">Сообщение от System.Net.Mail</div>"; //тело сообщения
                message.Attachments.Add(new Attachment("... путь к файлу ...")); //добавить вложение к письму при необходимости

                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com")) //используем сервера Google
                {
                    client.Credentials = new NetworkCredential("mail@gmail.com", "secret"); //логин-пароль от аккаунта
                    client.Port = 587; //порт 587 либо 465
                    client.EnableSsl = true; //SSL обязательно

                    client.Send(message);
                }
            }
            catch (Exception e)
            {
              
            }
        }

        //MailKit.Net.Smtp.SmtpClient
        public async Task SendEmailAsync(string email, string subject, string content)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Моя компания", "vmukhametova@fil-it.ru")); //отправитель сообщения
                message.To.Add(new MailboxAddress(Encoding.UTF8, email, email)); //адресат сообщения
                message.Subject = subject; //тема сообщения
                message.Body = new BodyBuilder() { HtmlBody = content }.ToMessageBody(); //тело сообщения (так же в формате HTML)

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("mail.phoenixit.ru", 587, SecureSocketOptions.StartTls); //либо использум порт 465
                    await client.AuthenticateAsync("vmukhametova_fi", "Seventeen1$"); //логин-пароль от аккаунта
                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
