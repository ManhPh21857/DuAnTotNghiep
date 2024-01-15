using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
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
	                Id			   AS Id
                   ,customer_name  AS CustomerName
                   ,phone_number   AS PhoneNumber
                   ,City		   AS City
                   ,District	   AS District
                   ,Commune		   AS Commune
                   ,address_detail AS AddressDetail
                   ,is_default	   AS IsDefault
                   ,data_version   AS DataVersion
                FROM
	                customer_address
                WHERE
	                customer_id = @CustomerId
	                AND is_deleted = 0
            ";

            var result = await connect.QueryAsync<CustomerAddress>(query,
                new
                {
                    CustomerId = customerId
                }
            );

            return result;
        }

        public async Task InsertCustomerAddress(UpdateCustomerAddressParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                INSERT INTO [dbo].[customer_address] (
	                [customer_id]
                   ,[customer_name]
                   ,[phone_number]
                   ,[city]
                   ,[district]
                   ,[commune]
                   ,[address_detail]
                )
                VALUES (
	                @CustomerId
                   ,@CustomerName
                   ,@PhoneNumber
                   ,@City
                   ,@District
                   ,@Commune
                   ,@AddressDetail
                )
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    CustomerId = param.CustomerId,
                    CustomerName = param.CustomerName,
                    PhoneNumber = param.PhoneNumber,
                    City = param.City,
                    District = param.District,
                    Commune = param.Commune,
                    AddressDetail = param.AddressDetail
                }
            );
        }

        public async Task UpdateCustomerAddress(UpdateCustomerAddressParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE [dbo].[customer_address]
                SET
	                [customer_name]	 = @CustomerName
                   ,[phone_number]	 = @PhoneNumber
                   ,[city]			 = @City
                   ,[district]		 = @District
                   ,[commune]		 = @Commune
                   ,[address_detail] = @AddressDetail
                WHERE
	                [id] = @Id
	                AND [customer_id] = @CustomerId
	                AND [data_version] = @DataVersion
            ";

            var result = await connect.ExecuteAsync(command,
                new
                {
                    Id = param.Id,
                    CustomerId = param.CustomerId,
                    CustomerName = param.CustomerName,
                    PhoneNumber = param.PhoneNumber,
                    City = param.City,
                    District = param.District,
                    Commune = param.Commune,
                    AddressDetail = param.AddressDetail,
                    DataVersion = param.DataVersion,
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateDefaultAddress(int customerId, int id)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE customer_address
                SET
	                is_default = 0
                WHERE
	                customer_id = @CustomerId
	                AND is_deleted = 0
            ";

            await connect.ExecuteAsync(command, new { CustomerId = customerId });

            const string command2 = @"
                UPDATE customer_address
                SET
	                is_default = 1
                WHERE
	                customer_id = @CustomerId
	                AND id = @Id
	                AND is_deleted = 0
            ";

            int result = await connect.ExecuteAsync(command2,
                new
                {
                    Id = id,
                    CustomerId = customerId
                }
            );

            result.IsOptimisticLocked();
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
                   ,c.data_version AS DataVersion
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

        public async Task<string?> GetCustomerEmail(int customerId)
        {
            await using var connect = await this.provider.Connect();
            
            const string query = @"
                SELECT
                    email AS Email
                FROM
	                customers
                WHERE
	                is_deleted = 0
	                AND id = @CustomerId
            ";

            var result = await connect.QueryFirstOrDefaultAsync<string?>(query,
                new
                {
                    CustomerId = customerId
                }
            );
            
            return result;
        }

        public async Task UpdateCustomer(UpdateCustomerParam param)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                UPDATE customers
                SET
	                last_name	 = @LastName
                   ,first_name	 = @FirstName
                   ,phone_number = @PhoneNumber
                   ,birthday	 = @Birthday
                   ,image		 = @Image
                   ,sex			 = @Sex
                WHERE
	                id = @Id
	                AND is_deleted = 0
	                AND data_version = @DataVersion
            ";

            var result = await connect.ExecuteAsync(query,
                new
                {
                    LastName = param.LastName,
                    FirstName = param.FirstName,
                    PhoneNumber = param.PhoneNumber,
                    Birthday = param.Birthday,
                    Image = param.Image,
                    Sex = param.Sex,
                    Id = param.Id,
                    DataVersion = param.DataVersion
                }
            );

            result.IsOptimisticLocked();
        }
    }
}
