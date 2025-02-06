using MailKit.Net.Smtp;
using MimeKit;

namespace wsmcbl.src.controller.service;

public class EmailNotifierService
{
    private const string _smtpServer = "mail.cbl-edu.com";
    private const int _smtpPort = 587;
    private const string _smtpUser = "info@cbl-edu.com";

    public async Task sendEmail(string email, string subject, string mensaje)
    {
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(getTitle(), _smtpUser));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("html")
            {
                Text = mensaje
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls); 
                await client.AuthenticateAsync(_smtpUser, getPass());
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

            Console.WriteLine($"Mail sent to {email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending mail {ex.Message}");
        }
    }

    private string getTitle() => "Registro de calificaciones";

    private string getPass()
    {
        return "temporal";
    }
}