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
                                    [address]
                                )
                                VALUES (
                                   @Name
                                   ,@Address
                                   
                                )";
             await connect.ExecuteAsync(sql,new
             {

                 Name = Supplier.Name,
                 Address = Supplier.Address
             });
        }
        public async Task UpdateSupplier(SupplierInfo Supplier)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                UPDATE [suppliers]
                                SET 
                                name = @Name, 
                                address = @Address
                                WHERE id = @Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = Supplier.Id,
                Name = Supplier.Name,
                Address = Supplier.Address,
            });
        }
        public async Task<IEnumerable<SupplierInfo>> GetSupplier()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                select 
                                id As Id, 
                                name As Name,
                                address As Address
                                from 
                                [suppliers]
								where
                                is_deleted = 0
                                ";
            var result = await connect.QueryAsync<SupplierInfo>(sql);
            return result;
        }

        public async Task DeleteSupplier(SupplierInfo Supplier)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                UPDATE [suppliers]
                                SET 
                                [is_deleted] =1
                                WHERE id =@Id
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = Supplier.Id,
            
            });
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
    }
}
