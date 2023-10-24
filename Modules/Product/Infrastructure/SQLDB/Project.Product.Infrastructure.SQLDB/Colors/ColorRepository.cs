using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Colors;
using Project.Product.Domain.Enums;
using System.Drawing;

namespace Project.Product.Infrastructure.SQLDB.Colors
{
    public class ColorRepository : IColorRepository
    {
        private readonly ConnectionProvider provider;

        public ColorRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IEnumerable<ColorInfo>> GetColors(int? id)
        {
            var connect = await provider.Connect();

            var builder = new SqlBuilder();

            const string query = @"
                SELECT
	                [id]            AS Id
                   ,[color]         AS Color
                   ,[is_deleted]    AS IsDeleted
                   ,[data_version]  AS DataVersion
                FROM
	                [colors]
                /**where**/
            ";

            var template = builder.AddTemplate(query);

            if (id.HasValue)
            {
                builder.Where("[id] = @Id", new { Id = id });
            }

            var result = await connect.QueryAsync<ColorInfo>(template.RawSql);

            return result;
        }

        public async Task CreateColor(ColorInfo param)
        {
            var connect = await provider.Connect();

            const string query = @"
                INSERT [dbo].[colors] (
	                [color]
                )
                VALUES (
	                @Color
                )
            ";

            await connect.ExecuteAsync(query,
                new
                {
                    Color = param.Color
                }
            );
        }

        public async Task UpdateColor(ColorInfo param)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[colors]
                SET 
                    [color] = @Color
                WHERE
                    [id] = @Id
                    AND [data_version] = @DataVersion
                    AND [is_deleted] = @IsDeleted
            ";

            int result = await connect.ExecuteAsync(query,
                new
                {
                    Color = param.Color,
                    Id = param.Id,
                    DataVersion = param.DataVersion,
                    IsDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task DeleteColor(ColorInfo param)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[colors]
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
                    Id = param.Id,
                    DataVersion = param.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task ReActiveColor(ColorInfo param)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[colors]
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
                    Id = param.Id,
                    DataVersion = param.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }
    }
}