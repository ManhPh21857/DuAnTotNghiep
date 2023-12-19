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

        public async Task<int?> GetCustomerId(int userId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                id AS Id
                FROM
	                customers
                WHERE
	                user_id = @UserId
                    AND is_deleted = 0
            ";

            int? customerId = await connect.QueryFirstOrDefaultAsync<int?>(query,
                new
                {
                    UserId = userId
                }
            );

            return customerId;
        }

        public async Task<IEnumerable<CustomerAddress>> GetCustomerAddress(int customerId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]			 AS Id
                   ,[City]			 AS City
                   ,[District]		 AS District
                   ,[Commune]		 AS Commune
                   ,[address_detail] AS AddressDetail
                   ,[data_version]	 AS DataVersion
                FROM
	                [dbo].[customer_address]
                WHERE
	                customer_id = @CustomerId
            ";

            var result = await connect.QueryAsync<CustomerAddress>(query,
                new
                {
                    CustomerId = customerId
                }
            );

            return result;
        }

        public async Task<CustomerInfo> GetCustomerInfo(int userId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                c.Id		   AS Id
                   ,c.first_name   AS FirstName
                   ,c.last_name	   AS LastName
                   ,c.Image		   AS Image
                   ,c.Birthday	   AS Birthday
                   ,c.Sex		   AS Sex
                   ,c.phone_number AS PhoneNumber
                   ,c.user_id	   AS UserId
                   ,u.user_name	   AS UserName
                   ,email		   AS Email
                FROM
	                customers AS c
	                LEFT JOIN users AS u
		                ON c.user_id = u.id
                WHERE
	                c.is_deleted = 0
	                AND u.is_deleted = 0
	                AND user_id = @UserId
            ";

            var result = await connect.QueryFirstOrDefaultAsync<CustomerInfo>(query,
                new
                {
                    UserId = userId
                }
            );

            return result;
        }
    }
}
