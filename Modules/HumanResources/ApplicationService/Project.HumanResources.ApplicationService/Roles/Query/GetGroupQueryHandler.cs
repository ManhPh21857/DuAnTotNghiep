using Project.Core.ApplicationService.Queries;
using Project.Core.Domain.Constants;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Integration.Roles.Query;

namespace Project.HumanResources.ApplicationService.Roles.Query
{
    public class GetGroupQueryHandler : QueryHandler<GetGroupQuery, GetGroupQueryResult>
    {
        private readonly IRoleRepository roleRepository;

        public GetGroupQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async override Task<GetGroupQueryResult> Handle(
            GetGroupQuery request,
            CancellationToken cancellationToken
        )
        {
            int skip = (request.Page - 1) * CommonConst.EMPLOYEE_PAGE_SIZE;
            int take = CommonConst.EMPLOYEE_PAGE_SIZE;
            var groups = await this.roleRepository.GetGroups(skip, take);
            int totalPage = groups.Item2 / CommonConst.EMPLOYEE_PAGE_SIZE;
            if (groups.Item2 % 2 != 0)
            {
                totalPage++;
            }

            return new GetGroupQueryResult(groups.Item1, totalPage);
        }
    }
}
