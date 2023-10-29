

using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Colors;
using Project.Product.Domain.Enums;
using Project.Product.Domain.Sizes;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Product.Infrastructure.SQLDB.Sizes
{
    public class SizeRepository : ISizeRepository
    {
        private readonly ConnectionProvider provider;

        public SizeRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task CreateSize(SizeInfo size)
        {
            var connect = await provider.Connect();

            const string query = @"
                INSERT [dbo].[sizes] (
	                [size]
                )
                VALUES (
	                @Size
                )
            ";

            await connect.ExecuteAsync(query,
                new
                {
                    Size = size.Size
                }
            );
        }

        public async Task DeleteSize(SizeInfo size)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[sizes]
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
                    Id = size.Id,
                    DataVersion = size.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<IEnumerable<SizeInfo>> GetSizes(int? id)
        {
            var connect = await provider.Connect();

            var builder = new SqlBuilder();

            const string query = @"
                SELECT
	                [id]            AS Id
                   ,[size]         AS Size
                   ,[is_deleted]    AS IsDeleted
                   ,[data_version]  AS DataVersion
                FROM
	                [sizes]
                /**where**/
            ";

            var template = builder.AddTemplate(query);

            if (id.HasValue)
            {
                builder.Where("[id] = @Id", new { Id = id });
            }

            var result = await connect.QueryAsync<SizeInfo>(template.RawSql);

            return result;
        }

        public async Task ReActiveSize(SizeInfo size)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[sizes]
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
                    Id = size.Id,
                    DataVersion = size.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateSize(SizeInfo size)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[sizes]
                SET 
                    [size] = @Size
                WHERE
                    [id] = @Id
                    AND [data_version] = @DataVersion
                    AND [is_deleted] = @IsDeleted
            ";

            int result = await connect.ExecuteAsync(query,
                new
                {
                    Size = size.Size,
                    Id = size.Id,
                    DataVersion = size.DataVersion,
                    IsDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }
    }
}
