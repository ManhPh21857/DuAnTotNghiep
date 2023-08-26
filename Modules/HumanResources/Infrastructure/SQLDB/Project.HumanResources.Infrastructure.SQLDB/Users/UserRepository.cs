using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.HumanResources.Domain.Users;

namespace Project.HumanResources.Infrastructure.SQLDB.Users;

public class UserRepository : IUserRepository {
    private readonly ConnectionProvider provider;

    public UserRepository(ConnectionProvider provider) {
        this.provider = provider;
    }
    public async Task<List<UserInfo>> GetUsers(int? id) {
        await using var connect = await provider.Connect();
        var builder = new SqlBuilder();

        const string query = @"
            SELECT
                id           AS Id
               ,[name]       AS Name
               ,phone_number AS PhoneNumber
               ,[address]    AS Address
               ,email        AS Email
            FROM
                [users]
            /**where**/";

        var selector = builder.AddTemplate(query);

        if(id.HasValue) {
            builder.Where("id = @Id", new { Id = id });
        }

        var result = (await connect.QueryAsync<UserInfo>(selector.RawSql, selector.Parameters)).ToList();
        return result;
    }
}