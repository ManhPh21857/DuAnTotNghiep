namespace Project.HumanResources.Domain.Users;

public interface IUserRepository
{
    Task<List<User>> GetUser(GetUserParam param);

    Task<List<string>> GetUserRoles(string uid);

    Task<int> RegisterUser(RegisterUserParam param);

    Task InsertUserInfo(InsertUserInfoParam param);

    Task InsertUserRole(InsertUserRoleParam param);

    Task<List<UserInfo>> GetUserInfo(int? id);
}