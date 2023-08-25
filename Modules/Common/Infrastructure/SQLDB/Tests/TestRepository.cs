using Dapper;
using Project.Common.Domain.Tests;
using Project.Core.Infrastructure.SQLDB.Providers;

namespace Project.Common.Infrastructure.SQLDB.Tests;

public class TestRepository : ITestRepository {
    private readonly ConnectionProvider provider;

    public TestRepository(ConnectionProvider provider) {
        this.provider = provider;
    }

    public async Task<TestInfo> GetTestInfo(int id) {
        await using var connect = await provider.Connect();

        string query = @"
                        SELECT
                            username as UserName
                        FROM
                            users
                        WHERE
                            id = @Id";

        var result = await connect.QueryFirstOrDefaultAsync<TestInfo>(query,
            new {
                Id = id
            });

        return result;
    }
}