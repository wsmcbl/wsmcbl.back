using MailKit.Net.Smtp;
using MimeKit;
using wsmcbl.src.exception;

namespace wsmcbl.src.controller.service;

public class EmailNotifierService
{
    private const string _smtpServer = "mail.cbl-edu.com";
    private const int _smtpPort = 587;
    private const string _smtpUser = "info@cbl-edu.com";

    public async Task sendEmail(List<string> emailList, string subject, string message)
    {
        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls); 
                await client.AuthenticateAsync(_smtpUser, getPassword());
                
                foreach (var email in emailList)
                {
                    var emailMessage = new MimeMessage();
                    emailMessage.From.Add(new MailboxAddress(getTitle(), _smtpUser));
                    emailMessage.To.Add(new MailboxAddress("", email));
                    emailMessage.Subject = subject;

                    emailMessage.Body = new TextPart("html")
                    {
                        Text = message
                    };
                    
                    await client.SendAsync(emailMessage);    
                }
                
                await client.DisconnectAsync(true);
            }

            var d = emailList.Aggregate("", (current, email) => current + $", {email}");
            Console.WriteLine($"Mail sent to {d}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending mail {ex.Message}");
        }
    }

    private static string getTitle() => "Registro de calificaciones";

    private static string getPassword()
    {
        var value = Environment.GetEnvironmentVariable("INFO_EMAIL_PASSWORD");
        if (value == null)
        {
            throw new InternalException("INFO_EMAIL_PASSWORD environment not found.");
        }

        return value;
    }
}