using Project.HumanResources.Domain.Users;

namespace Project.HumanResources.Integration.Users.Query;

public class UserQueryResult
{
    public UserInfo User { get; set; }

    public UserQueryResult(UserInfo user)
    {
        this.User = user;
    }
}