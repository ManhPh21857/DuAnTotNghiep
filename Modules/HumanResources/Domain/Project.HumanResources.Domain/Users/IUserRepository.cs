using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Domain.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUserLogin(GetUserParam param);
    Task<IEnumerable<User>> GetUserRegister(string? email, string? username);
    Task<IEnumerable<RoleInfo>> GetUserRoles(int id);
    Task UpdateUser(UpdateUserParam param);
    Task DeleteUserRoles(int userId);
    Task<int> RegisterUser(RegisterUserParam param);
    Task InsertUserRole(InsertUserRoleParam param);
    Task<UserInfo> GetUserInfo(int id);
    Task<IEnumerable<User>> GetEmployeeLogin(string username, string password, int roleId);
    Task ForgotPassword(string email, string newPassword);
    Task DeleteUser(int id);
}
