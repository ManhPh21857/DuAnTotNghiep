namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Login;

public class UserInformation
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}