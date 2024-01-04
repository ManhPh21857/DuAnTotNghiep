using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Integration.Roles.Query
{
    public class GroupQueryResult
    {
        public IEnumerable<GroupInfo> Groups { get; set; }
        public IEnumerable<GroupRoleInfo> GroupRoles { get; set; }

        public GroupQueryResult(IEnumerable<GroupInfo> groups, IEnumerable<GroupRoleInfo> groupRoles)
        {
            Groups = groups;
            GroupRoles = groupRoles;
        }
    }
}
