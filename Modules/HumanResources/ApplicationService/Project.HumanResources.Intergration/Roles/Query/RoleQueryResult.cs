using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Integration.Roles.Query
{
    public class RoleQueryResult
    {
        public IEnumerable<RoleInfo> Roles { get; set; }

        public RoleQueryResult(IEnumerable<RoleInfo> roles)
        {
            Roles = roles;
        }
    }
}
