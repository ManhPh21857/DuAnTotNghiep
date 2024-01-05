using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Integration.Roles.Query;

namespace Project.HumanResources.ApplicationService.Roles.Query
{
    public class GroupQueryHandler : QueryHandler<GroupQuery, GroupQueryResult>
    {
        private readonly IRoleRepository roleRepository;

        public GroupQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async override Task<GroupQueryResult> Handle(GroupQuery request, CancellationToken cancellationToken)
        {
            var group = await roleRepository.GetGroups();
            var groupRole = await roleRepository.GetGroupRoles();

            return new GroupQueryResult(group, groupRole);
        }
    }
}
