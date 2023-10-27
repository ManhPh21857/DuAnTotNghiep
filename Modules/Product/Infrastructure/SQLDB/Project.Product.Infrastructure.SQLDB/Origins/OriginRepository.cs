using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Enums;
using Project.Product.Domain.Origins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.SQLDB.Origins
{
    public class OriginRepository : IOriginRepository
    {
        private readonly ConnectionProvider connection;
        public OriginRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }

        public async Task<OriginInfo> CheckOriginName(string name)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                select 
                                name
                                from 
                                [origins]
                                where name = @Name
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<OriginInfo>(sql, new
            {
                Name = name
            });
            return result;
        }

        public async Task CreateOrigin(OriginInfo origin)
        {
            var connect = await connection.Connect();

            const string query = @"
                INSERT [dbo].[origins] (
	                [name]
                )
                VALUES (
	                @Name
                )
            ";

            await connect.ExecuteAsync(query,
                new
                {
                    Name = origin.Name
                }
            );
        }

        public async Task DeleteOrigin(OriginInfo origin)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[origins]
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
                    Id = origin.Id,
                    DataVersion = origin.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<IEnumerable<OriginInfo>> GetOrigin(int? id)
        {
            var connect = await connection.Connect();

            var builder = new SqlBuilder();

            const string query = @"
                SELECT
	                [id]            AS Id
                   ,[name]          AS Name
                   ,[is_deleted]    AS IsDeleted
                   ,[data_version]  AS DataVersion
                FROM
	                [origins]
                /**where**/
            ";

            var template = builder.AddTemplate(query);

            if (id.HasValue)
            {
                builder.Where("[id] = @Id", new { Id = id });
            }

            var result = await connect.QueryAsync<OriginInfo>(template.RawSql);

            return result;
        }

        public async Task ReactiveOrigin(OriginInfo origin)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[origins]
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
                    Id = origin.Id,
                    DataVersion = origin.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateOrigin(OriginInfo origin)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[origins]
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
                    Name = origin.Name,
                    Id = origin.Id,
                    DataVersion = origin.DataVersion,
                    IsDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }
    }
}
