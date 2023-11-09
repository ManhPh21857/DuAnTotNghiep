using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Domain.Users;

public interface IUserRepository
{
    Task<List<User>> GetUser(GetUserParam param);

    Task<IEnumerable<RoleInfo>> GetUserRoles(int id);

    Task<int> RegisterUser(RegisterUserParam param);

    Task InsertUserInfo(InsertUserInfoParam param);

    Task InsertUserRole(InsertUserRoleParam param);

    Task<List<UserInfo>> GetUserInfo(int? id);
}