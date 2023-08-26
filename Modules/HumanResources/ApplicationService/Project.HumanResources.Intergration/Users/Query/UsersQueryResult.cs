using Project.HumanResources.Domain.Users;

namespace Project.HumanResources.Integration.Users.Query;

public class UsersQueryResult {
    public List<UserInfo> Users { get; set; }

    public UsersQueryResult(List<UserInfo> users) {
        this.Users = users;
    }
}