using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.SQLDB.Materials
{
    public class MaterialsRepository : IMaterialsRepository
    {
        private readonly ConnectionProvider connection;
        public MaterialsRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }
        public async Task CreateMaterials(MaterialsInfo materials)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                INSERT [dbo].[materials] (
	                                [name]
                                )
                                VALUES (
                                   @Name
                                )";
            await connect.ExecuteAsync(sql, new
            {

                Name = materials.Name,
            });
        }

        public async Task DeleteMaterials(MaterialsInfo materials)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                Delete From [materials]
                                where Id=@Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = materials.Id,

            });
        }

        public async Task<IEnumerable<MaterialsInfo>> GetMaterials()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                select 
                                id As Id, 
                                name As Name
                                from 
                                [materials]
                                ";
            var result = await connect.QueryAsync<MaterialsInfo>(sql);
            return result;
        }

        public async Task UpdateMaterials(MaterialsInfo materials)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                UPDATE [materials]
                                SET 
                                name = @Name
                                WHERE id = @Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = materials.Id,
                Name = materials.Name
            });
        }
    }
}
