using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Domain.Users;

namespace Project.HumanResources.Infrastructure.SQLDB.Users;

public class UserRepository : IUserRepository
{
    private readonly ConnectionProvider provider;

    public UserRepository(ConnectionProvider provider)
    {
        this.provider = provider;
    }

    public async Task<User> CheckUserExistence(int id)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            SELECT
	            [user_name] AS Username
               ,[Password]  AS Password
            FROM
	            [users]
            WHERE
	            [id] = @Id
	            AND [is_deleted] = 0
            ";

        var result = await connect.QueryFirstOrDefaultAsync<User>(query,
            new
            {
                Id = id
            }
        );

        return result;
    }

    public async Task<IEnumerable<User>> GetUserLogin(GetUserParam param)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            SELECT
                [id]            AS Id
               ,[user_name]     AS Username
               ,[is_deleted]    AS IsDeleted
            FROM
	            [users]
            WHERE
	            (
                    [user_name] = @Username
                    OR [email] = @Username
                )
	            AND [password] = @Password
                AND [is_deleted] = @IsDeleted
            ";

        var result = await connect.QueryAsync<User>(query,
            new
            {
                Username = param.Username,
                Password = param.Password,
                IsDeleted = 0
            }
        );

        return result;
    }

    public async Task<IEnumerable<User>> GetUserRegister(string? email, string? username)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            SELECT
                [id]            AS Id
               ,[user_name]     AS Username
               ,[is_deleted]    AS IsDeleted
            FROM
	            [users]
            WHERE
	            [user_name] = @Username
	            OR [email] = @Email
            ";

        var result = await connect.QueryAsync<User>(query,
            new
            {
                Username = username,
                Email = email
            }
        );

        return result;
    }

    public async Task<IEnumerable<RoleInfo>> GetUserRoles(int id)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            SELECT
                r.[id]          AS Id
	           ,r.[role_name]   AS Name
            FROM
                [users] AS u
                LEFT JOIN [user_roles] AS ur
                    ON u.[id] = ur.[user_id]
	            JOIN [roles] AS r
		            ON ur.[role_id] = r.[id]
            WHERE
	            u.[id] = @Id
                AND u.[is_deleted] = 0";

        var result = await connect.QueryAsync<RoleInfo>(query,
            new
            {
                Id = id
            }
        );

        return result;
    }

    public async Task UpdateUser(UpdateUserParam param)
    {
        await using var connect = await this.provider.Connect();

        const string command = @"
            UPDATE users
            SET
	            password = @Password
            WHERE
	            id = @Id
	            AND is_deleted = 0
	            AND data_version = @DataVersion
        ";

        var result = await connect.ExecuteAsync(command,
            new
            {
                Password = param.Password,
                Id = param.Id,
                DataVersion = param.DataVersion
            }
        );

        result.IsOptimisticLocked();
    }

    public async Task DeleteUserRoles(int userId)
    {
        await using var connect = await this.provider.Connect();

        const string command = @"
            DELETE user_roles
            WHERE
	            [user_id] = @UserId
        ";

        await connect.ExecuteAsync(command,
            new
            {
                UserId = userId
            }
        );
    }

    public async Task<int> RegisterUser(RegisterUserParam param)
    {
        await using var connect = await this.provider.Connect();

        const string command = @"
            INSERT [dbo].[users] (
               [email]
               ,[user_name]
               ,[password]
            )
            VALUES (
               @Email
               ,@Username
               ,@Password
            )
            SELECT @@IDENTITY AS Id";

        var result = await connect.QuerySingleOrDefaultAsync<int>(command,
            new
            {
                Email = param.Email,
                UserName = param.Username,
                Password = param.Password
            });

        return result;
    }

    public async Task InsertUserRole(InsertUserRoleParam param)
    {
        await using var connect = await this.provider.Connect();

        const string command = @"
            INSERT [dbo].[user_roles] (
	            [user_id]
               ,[role_id]
            )
            VALUES (
	            @UserId
               ,@RoleId
            )";

        await connect.ExecuteAsync(command,
            new
            {
                UserId = param.UserId,
                RoleId = param.Role
            });
    }

    public async Task<UserInfo> GetUserInfo(int id)
    {
        await using var connect = await this.provider.Connect();
        const string query = @"
            SELECT
	            u.[user_name]   AS Username
               ,ui.[first_name] AS FirstName
               ,ui.[last_name]  AS LastName
               ,ui.[image]		AS Image
            FROM
	            [users] AS u
	            LEFT JOIN [user_infos] AS ui
		            ON u.[id] = ui.[user_id]
            WHERE
	            id = @Id";

        var result = await connect.QuerySingleOrDefaultAsync<UserInfo>(query,
            new
            {
                Id = id
            }
        );

        return result;
    }

    public async Task<IEnumerable<User>> GetEmployeeLogin(string username, string password, int roleId)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            SELECT
	            u.[Id]		   AS Id
               ,u.[user_name]  AS Username
               ,u.[is_deleted] AS IsDeleted
            FROM
	            [users] AS u
	            JOIN [dbo].[user_roles] AS ur
		            ON u.[Id] = ur.[user_id]
            WHERE
	            u.[user_name] = @Username
	            AND u.[password] = @Password
	            AND u.[is_deleted] = 0
	            AND ur.[role_id] = @Role
            ";

        var result = await connect.QueryAsync<User>(query,
            new
            {
                Username = username,
                Password = password,
                Role = roleId
            }
        );

        return result;
    }

    public async Task ForgotPassword(string email, string newPassword)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            UPDATE [dbo].[users]
            SET
	            [password] = @NewPassword
            WHERE
	            [email] = @Email
	            AND [is_deleted] = 0
        ";

        var result = await connect.ExecuteAsync(query,
            new
            {
                Email = email,
                NewPassword = newPassword
            }
        );

        result.IsOptimisticLocked();
    }

    public async Task DeleteUser(int id)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE users
            SET
	            is_deleted = 1
            WHERE
	            id = @Id
        ";

        var result = await connect.ExecuteAsync(command,
            new
            {
                Id = id
            }
        );

        result.IsOptimisticLocked();
    }

    public async Task ChangePassword(int id, string oldPassword, string newPassword)
    {
        await using var connect = await this.provider.Connect();

        const string command = @"
            UPDATE [users]
            SET
	            [password] = @NewPassword
            WHERE
	            [id] = @Id
	            AND is_deleted = 0
	            AND [password] = @OldPassword
        ";

        var result = await connect.ExecuteAsync(command,
            new
            {
                Id = id,
                OldPassword = oldPassword,
                NewPassword = newPassword
            }
        );

        result.IsOptimisticLocked();
    }
}
