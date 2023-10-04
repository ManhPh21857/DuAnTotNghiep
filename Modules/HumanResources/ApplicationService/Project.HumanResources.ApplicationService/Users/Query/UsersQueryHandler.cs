using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Users.Query;

namespace Project.HumanResources.ApplicationService.Users.Query;

public class UsersQueryHandler : QueryHandler<UsersQuery, UsersQueryResult>
{
    private readonly IUserRepository userRepository;

    public UsersQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async override Task<UsersQueryResult> Handle(UsersQuery request, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetUserInfo(null);

        return new UsersQueryResult(result);
    }
}