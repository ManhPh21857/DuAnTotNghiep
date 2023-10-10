using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Manufacturers;
using Dapper;
namespace Project.Product.Infrastructure.SQLDB.Manufacturers
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ConnectionProvider connection;
        public ManufacturerRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }


        public async Task AddManufacturers(ManufacturerInfo manufacturer)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                INSERT [dbo].[manufacturers] (

	                                [name],
				                    [status]
                                )
                                VALUES (
                                   @Name
                                   ,@Status
                                )";
             await connect.ExecuteAsync(sql,new
             {

                 Name = manufacturer.Name,
                 Status = manufacturer.Status
             });
        }
        public async Task UpdateManufacturers(ManufacturerInfo manufacturer)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                UPDATE [manufacturers]
                                SET 
                                name = @Name, 
                                status= @Status
                                WHERE id = @Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Status = manufacturer.Status
            });
        }
        public async Task<IEnumerable<ManufacturerInfo>> GetManufacturers()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                select 
                                id, 
                                name,
                                status 
                                from 
                                [manufacturers]
                                ";
            var result = await connect.QueryAsync<ManufacturerInfo>(sql);
            return result;
        }

        public async Task DeleteManufacturers(ManufacturerInfo manufacturer)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                Delete From [manufacturers]
                                where Id=@Id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Id = manufacturer.Id,
            
            });
        }
    }
}
