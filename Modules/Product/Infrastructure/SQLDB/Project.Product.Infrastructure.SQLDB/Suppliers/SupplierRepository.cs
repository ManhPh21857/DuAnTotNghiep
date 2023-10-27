using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Suppliers;
using Dapper;
using Project.Product.Domain.Enums;
using Project.Core.Infrastructure.SQLDB.Extensions;

namespace Project.Product.Infrastructure.SQLDB.Suppliers
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ConnectionProvider connection;
        public SupplierRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }
        public async Task AddSupplier(SupplierInfo Supplier)
        {
            var connect = await connection.Connect();

            const string query = @"
                INSERT [dbo].[suppliers] (
	                [name],
                    [address]
                )
                VALUES (
	                @Name,
                    @Address
                )
            ";

            await connect.ExecuteAsync(query,
                new
                {
                    Name = Supplier.Name,
                    Address = Supplier.Address
                }
            );
        }
        public async Task UpdateSupplier(SupplierInfo Supplier)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[suppliers]
                SET 
                    [name] = @Name,
                    [address] = @Address
                WHERE
                    [id] = @Id
                    AND [data_version] = @DataVersion
                    AND [is_deleted] = @IsDeleted
            ";

            int result = await connect.ExecuteAsync(query,
                new
                {
                    Name = Supplier.Name,
                    Id = Supplier.Id,
                    Address = Supplier.Address,
                    DataVersion = Supplier.DataVersion,
                    IsDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task DeleteSupplier(SupplierInfo Supplier)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[suppliers]
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
                    Id = Supplier.Id,
                    DataVersion = Supplier.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<SupplierInfo> CheckSupplierName(string name,string address)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                select 
                                name,
                                address
                                from 
                                [suppliers]
                                where name = @Name and address = @address
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<SupplierInfo>(sql, new
            {
                Name = name,
                Address = address
            });
            return result;
        }

        public async Task<IEnumerable<SupplierInfo>> GetSupplier(int? id)
        {
            var connect = await connection.Connect();

            var builder = new SqlBuilder();

            const string query = @"
                SELECT
	                [id]            AS Id
                   ,[name]          AS Name
                   ,[address]       As Address
                   ,[is_deleted]    AS IsDeleted
                   ,[data_version]  AS DataVersion
                FROM
	                [suppliers]
                /**where**/
            ";

            var template = builder.AddTemplate(query);

            if (id.HasValue)
            {
                builder.Where("[id] = @Id", new { Id = id });
            }

            var result = await connect.QueryAsync<SupplierInfo>(template.RawSql);

            return result;
        }

        public async Task ReactiveSupplier(SupplierInfo Supplier)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[suppliers]
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
                    Id = Supplier.Id,
                    DataVersion = Supplier.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }
    }
}
