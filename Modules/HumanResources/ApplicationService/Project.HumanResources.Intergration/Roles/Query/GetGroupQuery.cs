using Project.Core.ApplicationService.Queries;

namespace Project.HumanResources.Integration.Roles.Query
{
    public class GetGroupQuery : IQuery<GetGroupQueryResult>
    {
        public int Page { get; set; }

        public GetGroupQuery(int page)
        {
            this.Page = page;
        }
    }
}
