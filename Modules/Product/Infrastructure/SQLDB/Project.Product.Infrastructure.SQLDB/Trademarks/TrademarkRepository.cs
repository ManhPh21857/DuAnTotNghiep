﻿using Dapper;
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

                Name = trademark.Name
            });
        }

        public async Task DeleteTrademark(TrademarkInfo trademark)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                UPDATE [trademarks]
                                SET 
                                [is_deleted] =1
                                WHERE id =@Id
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
                                id As Id, 
                                name As Name
                                from 
                                [trademarks]
                                where
                                is_deleted = 0
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
