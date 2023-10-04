using Project.Core.ApplicationService.Commands;
using Project.Core.Domain.Enums;

namespace Project.HumanResources.Integration.Authentication.Register;

public class RegisterRequest : ICommand<RegisterResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsEmployee { get; set; }
    public List<Role> Roles { get; set; }

    public RegisterRequest(
        string userName,
        string password,
        string email,
        string firstName,
        string lastName,
        bool isEmployee,
        List<Role> roles
    )
    {
        this.UserName = userName;
        this.Password = password;
        this.Email = email;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.IsEmployee = isEmployee;
        this.Roles = roles;
    }
}