namespace Project.HumanResources.Domain.Roles
{
    public interface IRoleRepository
    {
        Task<IEnumerable<int>> GetGroupRole(int groupId);
        Task<IEnumerable<GroupInfo>> GetGroups();
        Task<(IEnumerable<GroupInfo>, int)> GetGroups(int skip, int take);
        Task<IEnumerable<RoleInfo>> GetRoles();
        Task<IEnumerable<GroupRoleInfo>> GetGroupRoles();
        Task<int> CreateGroup(string name, string description);
        Task<int> DeleteGroup(int id, byte[]? dataVersion);
        Task UpdateGroup(int id, string name, string description, byte[]? dataVersion);
        Task CreateGroupRole(int groupId, int roleId);
        Task DeleteGroupRole(int groupId);
        Task<(GroupInfo, IEnumerable<RoleInfo>)> GetGroupRoles(int groupId);
    }
}
