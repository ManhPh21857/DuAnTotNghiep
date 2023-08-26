namespace Project.HumanResources.Domain.Users;

public interface IUserRepository {
    Task<List<UserInfo>> GetUsers(int? id);
}