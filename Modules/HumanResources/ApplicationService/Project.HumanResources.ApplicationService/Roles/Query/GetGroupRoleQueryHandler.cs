using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Integration.Roles.Query;

namespace Project.HumanResources.ApplicationService.Roles.Query
{
    public class GetGroupRoleQueryHandler : QueryHandler<GetGroupRoleQuery, GetGroupRoleQueryResult>
    {
        private readonly IRoleRepository roleRepository;

        public GetGroupRoleQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async override Task<GetGroupRoleQueryResult> Handle(
            GetGroupRoleQuery request,
            CancellationToken cancellationToken
        )
        {
            var response = await this.roleRepository.GetGroupRoles(request.Id);

            var group = response.Item1;

            return new GetGroupRoleQueryResult(
                group.Id,
                group.Name,
                group.Description,
                group.DataVersion,
                response.Item2
            );
        }
    }
}
