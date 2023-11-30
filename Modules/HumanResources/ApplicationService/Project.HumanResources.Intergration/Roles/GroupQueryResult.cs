using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Integration.Roles
{
    public class GroupQueryResult
    {
        public IEnumerable<GroupInfo> Groups { get; set; }
        public IEnumerable<GroupRoleInfo> GroupRoles { get; set; }

        public GroupQueryResult(IEnumerable<GroupInfo> groups, IEnumerable<GroupRoleInfo> groupRoles)
        {
            this.Groups = groups;
            this.GroupRoles = groupRoles;
        }
    }
}
