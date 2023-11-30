using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.HumanResources.Domain.Employees;

namespace Project.HumanResources.Infrastructure.SQLDB.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionProvider provider;

        public EmployeeRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployees(int skip, int take)
        {
            await using var connect = await this.provider.Connect();
            
            const string query = @"
                SELECT
	            	[Id]		   AS Id
                   ,[UID]		   AS UID
                   ,[first_name]   AS FirstName
                   ,[last_name]	   AS LastName
                   ,[Image]		   AS Image
                   ,[Address]	   AS Address
                   ,[Birthday]	   AS Birthday
                   ,[Sex]		   AS Sex
                   ,[phone_number] AS PhoneNumber
                   ,[user_id]	   AS UserId
                   ,[data_version] AS DataVersion
                FROM
	            	[dbo].[employees]
                WHERE
	            	[is_deleted] = 0
                ORDER BY
	                id
                OFFSET @Skip ROWS
                FETCH NEXT @Take ROWS ONLY
            ";

            var result = await connect.QueryAsync<EmployeeInfo>(query,
                new
                {
                    Skip = skip,
                    Take = take
                }
            );

            return result;
        }

        public async Task CreateEmployee(CreateEmployeeParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                INSERT INTO [dbo].[employees] (
	                [UID]
                   ,[first_name]
                   ,[last_name]
                   ,[image]
                   ,[address]
                   ,[birthday]
                   ,[sex]
                   ,[phone_number]
                   ,[user_id]
                )
                VALUES (
	                @UID
                   ,@FirstName
                   ,@LastName
                   ,@Image
                   ,@Address
                   ,@Birthday
                   ,@Sex
                   ,@PhoneNumber
                   ,@UserId
                )
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    UID = param.UID,
                    FirstName = param.FirstName,
                    LastName = param.LastName,
                    Image = param.Image,
                    Address = param.Address,
                    Birthday = param.Birthday,
                    Sex = param.Sex,
                    PhoneNumber = param.PhoneNumber,
                    UserId = param.UserId
                }
            );
        }
    }
}
