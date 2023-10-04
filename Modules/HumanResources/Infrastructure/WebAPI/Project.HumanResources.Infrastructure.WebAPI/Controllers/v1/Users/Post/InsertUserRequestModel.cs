namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Post;

public class InsertUserRequestModel
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Pont { get; set; }
}