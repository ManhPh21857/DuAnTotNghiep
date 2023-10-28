using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Enums;
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
             var connect = await connection.Connect();
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
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[materials]
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
                    Id = material.Id,
                    DataVersion = material.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<IEnumerable<MaterialInfo>> GetMaterial(int? id)
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
	                                [materials]
                                
                                ";
            var template = builder.AddTemplate(sql);

            if (id.HasValue)
            {
                builder.Where("[id] = @Id", new { Id = id });
            }

            var result = await connect.QueryAsync<MaterialInfo>(template.RawSql);

            return result;
        }

        public async Task ReActiveMaterial(MaterialInfo material)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[materials]
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
                    Id = material.Id,
                    DataVersion = material.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateMaterial(MaterialInfo materials)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[materials]
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
                    Name = materials.Name,
                    Id = materials.Id,
                    DataVersion = materials.DataVersion,
                    IsDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

    }
}
