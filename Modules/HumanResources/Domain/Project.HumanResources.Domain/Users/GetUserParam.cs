namespace Project.HumanResources.Domain.Users;

public class GetUserParam
{
    public string? UID { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int? IsDeleted { get; set; }
}