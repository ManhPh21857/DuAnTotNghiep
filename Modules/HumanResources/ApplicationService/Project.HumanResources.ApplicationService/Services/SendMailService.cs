using Project.Core.ApplicationService.Commands;
using Project.HumanResources.Integration.Services;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Project.HumanResources.ApplicationService.Services;

public class SendMailService : CommandHandler<SendMailRequest, SendMailResponse>
{
    private readonly IConfiguration configuration;

    public SendMailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async override Task<SendMailResponse> Handle(SendMailRequest request,
        CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            try
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(configuration["MailInfo:Email"]);

                foreach (var item in request.SendTo)
                {
                    mail.To.Add(item);
                }

                mail.Subject = request.Title;
                mail.IsBodyHtml = true;
                mail.Body = request.Content;

                mail.Priority = MailPriority.High;
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(configuration["MailInfo:Email"],
                    configuration["MailInfo:Password"]);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                return new SendMailResponse(true);
            }
            catch (Exception)
            {
                return new SendMailResponse(false);
            }
        }, cancellationToken);
    }
}