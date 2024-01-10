using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
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

        public async Task<GetEmployeeModel> GetEmployees(int skip, int take)
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
                FETCH NEXT @Take ROWS ONLY;

                SELECT
	                COUNT([id]) AS TotalProduct
                FROM
	                [dbo].[employees]
                WHERE
	                [is_deleted] = 0
            ";

            var response = await connect.QueryMultipleAsync(query,
                new
                {
                    Skip = skip,
                    Take = take
                }
            );

            var result = new GetEmployeeModel
            {
                Employees = response.Read<EmployeeInfo>(),
                TotalEmployee = response.ReadFirstOrDefault<int>()
            };

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

        public async Task<EmployeeUser> GetEmployee(int id)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                e.[Id]			 AS Id
                   ,e.[UID]			 AS UID
                   ,e.[first_name]	 AS FirstName
                   ,e.[last_name]	 AS LastName
                   ,e.[Image]		 AS Image
                   ,e.[Address]		 AS Address
                   ,e.[Birthday]	 AS Birthday
                   ,e.[Sex]			 AS Sex
                   ,e.[phone_number] AS PhoneNumber
                   ,e.[data_version] AS EmployeeDataVersion
                   ,u.[Id]			 AS UserId
                   ,u.[Email]		 AS Email
                   ,u.[user_name]	 AS Username
                   ,u.[Password]	 AS Password
                   ,u.[data_version] AS UserDataVersion
                FROM
	                employees AS e
	                LEFT JOIN users AS u
		                ON e.[user_id] = u.[Id]
                WHERE
	                e.is_deleted = 0
	                AND e.Id = @Id
            ";

            var result = await connect.QueryFirstOrDefaultAsync<EmployeeUser>(query,
                new
                {
                    Id = id
                }
            );

            return result;
        }

        public async Task UpdateEmployee(UpdateEmployeeParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE [employees]
                SET
	                [first_name]   = @FirstName
                   ,[last_name]	   = @LastName
                   ,[phone_number] = @PhoneNumber
                   ,[sex]		   = @Sex
                   ,[birthday]	   = @Birthday
                   ,[image]		   = @Image
                   ,[address]	   = @Address
                WHERE
	                [id] = @Id
	                AND [data_version] = @DataVersion
                    AND [is_deleted] = 0
            ";

            var result =await connect.ExecuteAsync(command,
                new
                {
                    FirstName = param.FirstName,
                    LastName = param.LastName,
                    PhoneNumber = param.PhoneNumber,
                    Sex = param.Sex,
                    Birthday = param.Birthday,
                    Image = param.Image,
                    Address = param.Address,
                    Id = param.Id,
                    DataVersion = param.DataVersion
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task DeleteEmployee(DeleteEmployeeParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE employees
                SET
	                is_deleted = 1
                WHERE
	                id = @Id
	                AND data_version = @DataVersion
            ";

            var result = await connect.ExecuteAsync(command,
                new
                {
                    Id = param.Id,
                    DataVersion = param.DataVersion
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<int?> GetEmployeeId(int userId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id] AS Id
                FROM
	                [dbo].[employees]
                WHERE
	                [user_id] = @UserId
	                AND is_deleted = 0
            ";

            var result = await connect.QueryFirstOrDefaultAsync<int?>(query,
                new
                {
                    UserId = userId
                }
            );

            return result;
        }

        public async Task<IEnumerable<EmployeeOrder>> GetEmployeeOrder()
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
               SELECT
	                [Id]								   AS Id
                   ,CONCAT([last_name], ' ', [first_name]) AS FullName
                FROM
	                [dbo].[employees]
                WHERE
	                [is_deleted] = 0
                ORDER BY
	                FullName
            ";

            var result = await connect.QueryAsync<EmployeeOrder>(query);

            return result;
        }

        public async Task<EmployeeInfo> GetEmployeeInfo(int userId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Uid]	   AS UID
                   ,first_name AS FirstName
                   ,last_name  AS LastName
                   ,[Image]	   AS Image
                FROM
	                employees
                WHERE
	                user_id = @UserId
            ";

            var result = await connect.QueryFirstOrDefaultAsync<EmployeeInfo>(query,
                new
                {
                    UserId = userId
                }
            );

            return result;
        }
    }
}
