using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Integration.Roles
{
    public class RoleQueryResult
    {
        public IEnumerable<RoleInfo> Roles { get; set; }

        public RoleQueryResult(IEnumerable<RoleInfo> roles)
        {
            this.Roles = roles;
        }
    }
}
