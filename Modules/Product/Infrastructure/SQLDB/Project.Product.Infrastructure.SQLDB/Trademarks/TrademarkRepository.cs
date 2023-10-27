using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Enums;
using Project.Product.Domain.Trademarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.SQLDB.Trademarks
{
    public class TrademarkRepository : ITrademarkRepository
    {
        private readonly ConnectionProvider provider;

        public TrademarkRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<TrademarkInfo> CheckTrademarkName(string name)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                select 
                                name
                                from 
                                [trademarks]
                                where name = @Name 
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<TrademarkInfo>(sql, new
            {
                Name = name
            });
            return result;
        }

        public async Task CreateTrademark(TrademarkInfo trademark)
        {
            var connect = await provider.Connect();

            const string query = @"
                INSERT [dbo].[trademarks] (
	                [name]
                )
                VALUES (
	                @Name
                )
            ";

            await connect.ExecuteAsync(query,
                new
                {
                    Name = trademark.Name
                }
            );
        }

        public async Task DeleteTrademark(TrademarkInfo trademark)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[trademarks]
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
                    Id = trademark.Id,
                    DataVersion = trademark.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<IEnumerable<TrademarkInfo>> GetTrademark(int? id)
        {
            var connect = await provider.Connect();

            var builder = new SqlBuilder();

            const string query = @"
                SELECT
	                [id]            AS Id
                   ,[name]          AS Name
                   ,[is_deleted]    AS IsDeleted
                   ,[data_version]  AS DataVersion
                FROM
	                [trademarks]
                /**where**/
            ";

            var template = builder.AddTemplate(query);

            if (id.HasValue)
            {
                builder.Where("[id] = @Id", new { Id = id });
            }

            var result = await connect.QueryAsync<TrademarkInfo>(template.RawSql);

            return result;
        }

        public async Task ReactiveTrademark(TrademarkInfo trademark)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[trademarks]
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
                    Id = trademark.Id,
                    DataVersion = trademark.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateTrademark(TrademarkInfo trademark)
        {
            var connect = await provider.Connect();

            const string query = @"
                UPDATE [dbo].[trademarks]
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
                    Name = trademark.Name,
                    Id = trademark.Id,
                    DataVersion = trademark.DataVersion,
                    IsDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }
    }
}
