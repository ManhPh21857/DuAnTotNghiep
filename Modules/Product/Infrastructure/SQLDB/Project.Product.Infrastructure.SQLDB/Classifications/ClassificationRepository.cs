using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Classifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.SQLDB.Classification
{
    public class ClassificationRepository : IClassificationRepository
    {
        private readonly ConnectionProvider connection;
        public ClassificationRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }

        public async Task<ClassificationInfo> CheckClassificationName(string name)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                select 
                                name
                                from 
                                [classification]
                                where name = @Name
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<ClassificationInfo>(sql, new
            {
                Name = name
            });
            return result;
        }

        public async Task AddClassifications(ClassificationInfo classifications)
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

                Name = classifications.Name,
            });
        }

        public async Task DeleteClassifications(ClassificationInfo classifications)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                Delete From [classifications]
                                where Id=@Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = classifications.Id

            });
        }

        public async Task<IEnumerable<ClassificationInfo>> GetClassifications()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                select 
                                id As Id, 
                                name As Name
                                from 
                                [classifications]
                                ";
            var result = await connect.QueryAsync<ClassificationInfo>(sql);
            return result;
        }

        public async Task UpdateClassifications(ClassificationInfo classifications)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                UPDATE [classifications]
                                SET 
                                name = @Name
                                WHERE id = @Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = classifications.Id,
                Name = classifications.Name
            });
        }

    }
}
