using Dapper;
using Project.Core.Domain.User;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.HumanResources.Domain.Customers;

namespace Project.HumanResources.Infrastructure.SQLDB.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConnectionProvider provider;

        public CustomerRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }
        
        public async Task InsertCustomer(InsertCustomerParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                INSERT INTO [dbo].[customers] (
	                [UID]
                   ,[user_id]
                )
                VALUES (
	                @UID
                   ,@UserId
                )
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    UID = param.UID,
                    UserId = param.UserId
                }
            );
        }
    }
}
