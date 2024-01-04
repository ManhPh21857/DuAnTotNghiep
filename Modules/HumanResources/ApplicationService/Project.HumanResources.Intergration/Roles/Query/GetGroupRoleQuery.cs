using Project.Core.ApplicationService.Queries;

namespace Project.HumanResources.Integration.Roles.Query
{
    public class GetGroupRoleQuery : IQuery<GetGroupRoleQueryResult>
    {
        public int Id { get; set; }

        public GetGroupRoleQuery(int id)
        {
            this.Id = id;
        }
    }
}
