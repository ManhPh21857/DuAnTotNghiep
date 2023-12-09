using Project.Core.ApplicationService.Commands;
using Project.Core.Domain.Enums;

namespace Project.HumanResources.Integration.Authentication.VerifyEmail;

public class VerifyEmailRequest : ICommand<VerifyEmailResponse>
{
    public string Email { get; set; }
    public SendMailMode Mode { get; set; }

    public VerifyEmailRequest(string email, SendMailMode mode)
    {
        this.Email = email;
        this.Mode = mode;
    }
}