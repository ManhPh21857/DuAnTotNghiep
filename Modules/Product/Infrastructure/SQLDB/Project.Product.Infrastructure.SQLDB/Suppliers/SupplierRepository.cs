using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Suppliers;
using Dapper;
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
            await using var connect = await connection.Connect();
            const string sql = @"
                                INSERT [dbo].[suppliers] (

	                                [name],
                                    [address],
				                    [status]
                                )
                                VALUES (
                                   @Name
                                   ,@Address
                                   ,@Status
                                   
                                )";
             await connect.ExecuteAsync(sql,new
             {

                 Name = Supplier.Name,
                 Address = Supplier.Address,
                 Status = Supplier.Status,
             });
        }
        public async Task UpdateSupplier(SupplierInfo Supplier)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                UPDATE [suppliers]
                                SET 
                                name = @Name, 
                                address = @Address,
                                status= @Status
                                WHERE id = @Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = Supplier.Id,
                Name = Supplier.Name,
                Address = Supplier.Address,
                Status = Supplier.Status
            });
        }
        public async Task<IEnumerable<SupplierInfo>> GetSupplier()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                select 
                                id As Id, 
                                name As Name,
                                address As Address,
                                status 
                                from 
                                [suppliers]
                                ";
            var result = await connect.QueryAsync<SupplierInfo>(sql);
            return result;
        }

        public async Task DeleteSupplier(SupplierInfo Supplier)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                Delete From [suppliers]
                                where Id=@Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = Supplier.Id,
            
            });
        }
    }
}
