using Project.Core.ApplicationService.Queries;

namespace Project.HumanResources.Integration.Users.Query;

public class UserQuery : IQuery<UserQueryResult> {
    public int Id { get; set; }

    public UserQuery(int id) {
        Id = id;
    }
}