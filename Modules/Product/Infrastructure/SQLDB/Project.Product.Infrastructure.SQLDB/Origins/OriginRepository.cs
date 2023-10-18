using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
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
        public async Task CreateOrigin(OriginInfo origin)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                INSERT [dbo].[origins] (
	                                [name]
                                )
                                VALUES (
                                   @Name
                                )";
            await connect.ExecuteAsync(sql, new
            {

                Name = origin.Name
            });
        }

        public async Task DeleteOrigin(OriginInfo origin)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                Delete From [origins]
                                where id=@Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = origin.Id,

            });
        }

        public async Task<IEnumerable<OriginInfo>> GetOrigin()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                select 
                                id As Id, 
                                name As Name
                                from 
                                [origins]";
            var result = await connect.QueryAsync<OriginInfo>(sql);
            return result;
        }

        public async Task UpdateOrigin(OriginInfo origin)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                UPDATE [origins]
                                SET 
                                name = @Name
                                WHERE id = @Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = origin.Id,
                Name = origin.Name
            });
        }
    }
}
