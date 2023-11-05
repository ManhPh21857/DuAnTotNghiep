using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Enums;
using Project.Product.Domain.Classifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.SQLDB.Classifications
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
                                [classifications]
                                where name = @Name
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<ClassificationInfo>(sql, new
            {
                Name = name
            });
            return result;
        }

        public async Task CreateClassification(ClassificationInfo classifications)
        {
            var connect = await connection.Connect();
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

        public async Task DeleteClassification(ClassificationInfo classification)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[classifications]
                SET
	                [is_deleted] = @IsDeleted
                WHERE
	                [id] = @Id
                    AND [data_version] = @DataVersion
                    AND [is_deleted] = @NotDeleted
            ";

            int result = await connect.ExecuteAsync(query,
                new
                {
                    IsDeleted = IsDeleted.Yes,
                    Id = classification.Id,
                    DataVersion = classification.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<IEnumerable<ClassificationInfo>> GetClassification(int? id)
        {
            var connect = await connection.Connect();
            var builder = new SqlBuilder();
            const string sql = @"
                                SELECT
	                                [id]            AS Id
                                   ,[name]          AS Name
                                   ,[is_deleted]    AS IsDeleted
                                   ,[data_version]  AS DataVersion
                                FROM
	                                [classifications]
                                
                                ";
            var template = builder.AddTemplate(sql);

            if (id.HasValue)
            {
                builder.Where("[id] = @Id", new { Id = id });
            }

            var result = await connect.QueryAsync<ClassificationInfo>(template.RawSql);

            return result;
        }

        public async Task ReActiveClassification(ClassificationInfo classification)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[classifications]
                SET
	                [is_deleted] = @NotDeleted
                WHERE
	                [id] = @Id
                    AND [data_version] = @DataVersion
                    AND [is_deleted] = @IsDeleted
            ";

            int result = await connect.ExecuteAsync(query,
                new
                {
                    IsDeleted = IsDeleted.Yes,
                    Id = classification.Id,
                    DataVersion = classification.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateClassification(ClassificationInfo classifications)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[classifications]
                SET 
                    [name] = @Name
                WHERE
                    [id] = @Id
                    AND [data_version] = @DataVersion
                    AND [is_deleted] = @IsDeleted
            ";

            int result = await connect.ExecuteAsync(query,
                new
                {
                    Name = classifications.Name,
                    Id = classifications.Id,
                    DataVersion = classifications.DataVersion,
                    IsDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

    }
}
