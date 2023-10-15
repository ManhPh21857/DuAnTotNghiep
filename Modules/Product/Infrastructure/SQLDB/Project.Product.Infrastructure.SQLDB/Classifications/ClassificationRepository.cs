using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Classifications;
using Dapper;
namespace Project.Product.Infrastructure.SQLDB.Classifications
{
    public class ClassificationRepository : IClassificationRepository
    {
        private readonly ConnectionProvider connection;
        public ClassificationRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }


        public async Task AddClassifications(ClassificationInfo classification)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                INSERT [dbo].[classifications] (

	                                [name]
                                )
                                VALUES (
                                   @Name
                                )";
            await connect.ExecuteAsync(sql, new
            {

                Name = classification.Name
            });
        }
        public async Task UpdateClassifications(ClassificationInfo classification)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                UPDATE [manufacturers]
                                SET 
                                name = @Name
                                WHERE id = @Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = classification.Id,
                Name = classification.Name,
            });
        }
        public async Task<IEnumerable<ClassificationInfo>> GetClassifications()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                select 
                                id, 
                                name
                                from 
                                [classifications]
                                ";
            var result = await connect.QueryAsync<ClassificationInfo>(sql);
            return result;
        }

        public async Task DeleteClassifications(ClassificationInfo Classification)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                Delete From [classifications]
                                where Id=@Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = Classification.Id,

            });
        }
    }
}
