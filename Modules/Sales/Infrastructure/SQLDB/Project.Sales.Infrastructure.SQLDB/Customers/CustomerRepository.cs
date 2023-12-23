using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.Customers;

namespace Project.Sales.Infrastructure.SQLDB.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConnectionProvider provider;

        public CustomerRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }
        public async Task<IEnumerable<CustomerInfo>> GetCustomer()
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT 
                u.id,
                ui.first_name+' '+ui.last_name AS Name, 
                ui.phone_number AS PhoneNumber, u.user_name, u.email, r.role_name
                FROM users AS u
                LEFT JOIN user_infos AS ui on ui.user_id = u.id
                LEFT JOIN user_roles AS ur on u.id = ur.user_id
                LEFT JOIN roles AS r on r.id = u.id
				Group by u.id, ui.first_name+' '+ui.last_name ,ui.phone_number,u.user_name,u.email,r.role_name
            ";
            var result = await connect.QueryAsync<CustomerInfo>(sql);
            return result;
        }
    }
}
