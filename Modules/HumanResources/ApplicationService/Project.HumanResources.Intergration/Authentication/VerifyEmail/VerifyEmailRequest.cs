using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Authentication.VerifyEmail;

public class VerifyEmailRequest : ICommand<VerifyEmailResponse>
{
    public string Email { get; set; }

    public VerifyEmailRequest(string email)
    {
        Email = email;
    }
}