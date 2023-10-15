using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
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

        public async Task CreateTrademark(TrademarkInfo trademark)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                INSERT [dbo].[trademarks] (
	                                [name]
                                )
                                VALUES (
                                   @Name
                                )";
            await connect.ExecuteAsync(sql, new
            {

                Name = trademark.Name,
            });
        }


        public async Task DeleteTrademark(TrademarkInfo trademark)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                Delete From [trademarks]
                                where Id=@Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = trademark.Id,

            });
        }

        public async Task<IEnumerable<TrademarkInfo>> GetTrademark()
        {
            var connect = await provider.Connect();
            const string sql = @"
                                select 
                                id, 
                                name
                                from 
                                [trademarks]
                                ";
            var result = await connect.QueryAsync<TrademarkInfo>(sql);
            return result;
        }

        public async Task UpdateTrademark(TrademarkInfo trademark)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                UPDATE [trademarks]
                                SET 
                                name = @Name
                                WHERE id = @Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = trademark.Id,
                Name = trademark.Name
            });
        }
    }
}
