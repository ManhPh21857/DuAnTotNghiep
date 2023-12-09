using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Integration.Roles;

namespace Project.HumanResources.ApplicationService.Roles
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
            var group = await this.roleRepository.GetGroups();
            var groupRole = await this.roleRepository.GetGroupRoles();

            return new GroupQueryResult(group, groupRole);
        }
    }
}
