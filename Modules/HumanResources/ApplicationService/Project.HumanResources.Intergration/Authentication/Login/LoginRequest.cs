using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Authentication.Login;

public class LoginRequest : ICommand<LoginResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public LoginRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}