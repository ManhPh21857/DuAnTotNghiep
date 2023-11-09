using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Users.Query;

namespace Project.HumanResources.ApplicationService.Users.Query;

public class UserQueryHandler : QueryHandler<UserQuery, UserQueryResult>
{
    private readonly IUserRepository userRepository;
    private readonly ISessionInfo sessionInfo;

    public UserQueryHandler(IUserRepository userRepository, ISessionInfo sessionInfo)
    {
        this.userRepository = userRepository;
        this.sessionInfo = sessionInfo;
    }

    public async override Task<UserQueryResult> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        int id = sessionInfo.UserId.value;

        var result = (await userRepository.GetUserInfo(id)).FirstOrDefault();

        return new UserQueryResult(result!);
    }
}