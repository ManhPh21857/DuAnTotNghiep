using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Integration.Roles.Query
{
    public class GetGroupQueryResult
    {
        public IEnumerable<GroupInfo> Groups { get; set; }
        public int TotalPage { get; set; }

        public GetGroupQueryResult(IEnumerable<GroupInfo> groups, int totalPage)
        {
            this.Groups = groups;
            this.TotalPage = totalPage;
        }
    }
}
