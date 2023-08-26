using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Users.Query;

namespace Project.HumanResources.ApplicationService.Users.Query;

public class UserQueryHandler : QueryHandler<UserQuery, UserQueryResult> {
    private readonly IUserRepository userRepository;

    public UserQueryHandler(IUserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public async override Task<UserQueryResult> Handle(UserQuery request, CancellationToken cancellationToken) {
        var result = (await userRepository.GetUsers(request.Id)).FirstOrDefault();

        return new UserQueryResult(result);
    }
}