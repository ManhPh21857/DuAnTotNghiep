using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Integration.Roles.Query;

namespace Project.HumanResources.ApplicationService.Roles.Query
{
    public class RoleQueryHandler : QueryHandler<RoleQuery, RoleQueryResult>
    {
        private readonly IRoleRepository roleRepository;

        public RoleQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async override Task<RoleQueryResult> Handle(RoleQuery request, CancellationToken cancellationToken)
        {
            var result = await roleRepository.GetRoles();

            return new RoleQueryResult(result);
        }
    }
}
