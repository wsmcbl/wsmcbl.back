using MailKit.Net.Smtp;
using MimeKit;
using wsmcbl.src.exception;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service;

public class EmailNotifierService
{
    private const string TITLE = "Colegio Bautista Libertad";
    
    private const string _smtpServer = "mail.cbl-edu.com";
    private const int _smtpPort = 587;
    private const string _smtpUser = "info@cbl-edu.com";

    private string PASSWORD { get; set; } = null!;
    
    public async Task sendEmail(List<string> emailList, string subject, string message)
    {
        if (!Utility.isInProductionEnvironment()) return;

        setPassword();
        
        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls); 
                await client.AuthenticateAsync(_smtpUser, PASSWORD);
                
                foreach (var email in emailList)
                {
                    var emailMessage = new MimeMessage();
                    emailMessage.From.Add(new MailboxAddress(TITLE, _smtpUser));
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

            var d = emailList.Aggregate("", (current, email) => current + $"{email}, ");
            Console.WriteLine($"Mail sent to {d}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending mail {ex.Message}");
        }
    }

    private void setPassword()
    {
        var result = Environment.GetEnvironmentVariable("INFO_EMAIL_PASSWORD");
        PASSWORD = result ?? throw new InternalException("INFO_EMAIL_PASSWORD environment not found.");
    }
}