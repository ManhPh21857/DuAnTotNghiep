using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Roles;

namespace Project.HumanResources.ApplicationService.Roles
{
    public class RoleQueryHandler : QueryHandler<RoleQuery, RoleQueryResult>
    {
        private readonly IUserRepository userRepository;
        private readonly ISessionInfo sessionInfo;

        public RoleQueryHandler(IUserRepository userRepository, ISessionInfo sessionInfo)
        {
            this.userRepository = userRepository;
            this.sessionInfo = sessionInfo;
        }

        public override Task<RoleQueryResult> Handle(RoleQuery request, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}
