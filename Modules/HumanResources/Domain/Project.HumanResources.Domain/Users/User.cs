namespace Project.HumanResources.Domain.Users;

public class User
{
    public string UID { get; set; }
    public string Username { get; set; }
    public int IsDeleted { get; set; }
}