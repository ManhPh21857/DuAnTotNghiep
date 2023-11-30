using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Authentication.Register;

public class RegisterRequest : ICommand<RegisterResponse>
{
    public string Email { get; set; }
    public string Code { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public RegisterRequest(
        string email,
        string code,
        string userName,
        string password
    )
    {
        Email = email;
        Code = code;
        UserName = userName;
        Password = password;
    }
}