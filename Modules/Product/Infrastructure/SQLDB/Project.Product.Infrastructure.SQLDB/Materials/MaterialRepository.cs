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
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ConnectionProvider connection;
        public MaterialRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }

        public async Task<MaterialInfo> CheckMaterialName(string name)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                select 
                                name
                                from 
                                [materials]
                                where name = @Name
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<MaterialInfo>(sql, new
            {
                Name = name
            }) ;
            return result;
        }

        public async Task CreateMaterial(MaterialInfo materials)
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

        public async Task DeleteMaterial(MaterialInfo material)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                Delete From [materials]
                                where Id=@Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = material.Id

            });
        }

        public async Task<IEnumerable<MaterialInfo>> GetMaterial()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                select 
                                id As Id, 
                                name As Name
                                from 
                                [materials]
                                ";
            var result = await connect.QueryAsync<MaterialInfo>(sql);
            return result;
        }

        public async Task UpdateMaterial(MaterialInfo materials)
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
