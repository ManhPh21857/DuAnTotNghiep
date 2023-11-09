using Dapper;
using Microsoft.IdentityModel.Tokens;
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

    public async Task<User> CheckUserExistence(string username)
    {
        await using var connect = await provider.Connect();

        const string query = @"
            SELECT
                [user_name]     AS Username
               ,[is_deleted]    AS IsDeleted
            FROM
	            [users]
            WHERE
	            [user_name] = @Username";

        var result = await connect.QueryFirstOrDefaultAsync<User>(query,
            new
            {
                UserName = username,
            });

        return result;
    }

    public async Task<List<User>> GetUser(GetUserParam param)
    {
        await using var connect = await provider.Connect();

        var builder = new SqlBuilder();

        const string query = @"
            SELECT
                [id]            AS Id
	           ,[UID]           AS UID
               ,[user_name]     AS Username
               ,[is_deleted]    AS IsDeleted
            FROM
	            [users]
            WHERE
	            [user_name] = @Username
	            AND [password] = @Password";

        var selector = builder.AddTemplate(query);

        if (!param.Username.IsNullOrEmpty())
        {
            builder.Where("[user_name] = @Username", new { Username = param.Username });
        }

        if (!param.Password.IsNullOrEmpty())
        {
            builder.Where("[password] = @Password", new { Password = param.Password });
        }

        if (!param.UID.IsNullOrEmpty())
        {
            builder.Where("[UID] = @UID", new { UID = param.UID });
        }

        if (param.IsDeleted.HasValue)
        {
            builder.Where("[is_deleted] = @IsDeleted", new { IsDeleted = param.IsDeleted });
        }

        var result = (await connect.QueryAsync<User>(selector.RawSql, selector.Parameters)).ToList();

        return result;
    }

    public async Task<IEnumerable<RoleInfo>> GetUserRoles(int id)
    {
        await using var connect = await provider.Connect();

        const string query = @"
            SELECT
                r.[id]          AS Id
	           ,r.[role_name]   AS RoleName
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

    public async Task<int> RegisterUser(RegisterUserParam param)
    {
        await using var connect = await provider.Connect();

        const string command = @"
            INSERT [dbo].[users] (
	            [UID]
               ,[user_name]
               ,[password]
            )
            VALUES (
	            @UID
               ,@Username
               ,@Password
            )
            SELECT @@IDENTITY AS Id";

        var result = await connect.QuerySingleOrDefaultAsync<int>(command,
            new
            {
                UID = param.UID,
                UserName = param.Username,
                Password = param.Password
            });

        return result;
    }

    public async Task InsertUserInfo(InsertUserInfoParam param)
    {
        await using var connect = await provider.Connect();

        const string command = @"
            INSERT [dbo].[user_infos] (
	            [user_id]
               ,[email]
               ,[first_name]
               ,[last_name]
               ,[phone_number]
            )
            VALUES (
	            @UserId
               ,@Email
               ,@FirstName
               ,@LastName
               ,@PhoneNumber
            )";

        await connect.ExecuteAsync(command,
            new
            {
                UserId = param.UserId,
                Email = param.Email,
                FirstName = param.FirstName,
                LastName = param.LastName,
                PhoneNumber = param.PhoneNumber
            });
    }

    public async Task InsertUserRole(InsertUserRoleParam param)
    {
        await using var connect = await provider.Connect();

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

    public async Task<List<UserInfo>> GetUserInfo(int? id)
    {
        await using var connect = await provider.Connect();
        var builder = new SqlBuilder();

        const string query = @"
            SELECT
	            u.[user_id]		  AS UID
               ,u.[user_name]	  AS Username
               ,ui.[Email]		  AS Email
               ,ui.[first_name]	  AS FirstName
               ,ui.[last_name]	  AS LastName
               ,ui.[phone_number] AS PhoneNumber
            FROM
	            [users] AS u
	            LEFT JOIN [user_infos] AS ui
		            ON u.[user_id] = ui.[user_id]
            /**where**/";

        var selector = builder.AddTemplate(query);

        if (id.HasValue)
        {
            builder.Where("user_id = @Id", new { Id = id });
        }

        var result = (await connect.QueryAsync<UserInfo>(selector.RawSql, selector.Parameters)).ToList();

        return result;
    }
}