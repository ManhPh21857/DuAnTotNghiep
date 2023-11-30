namespace Project.HumanResources.Domain.Roles
{
    public interface IRoleRepository
    {
        Task<IEnumerable<int>> GetGroupRole(int groupId);

        Task<IEnumerable<GroupInfo>> GetGroups();

        Task<IEnumerable<RoleInfo>> GetRoles();
        Task<IEnumerable<GroupRoleInfo>> GetGroupRoles();
    }
}
