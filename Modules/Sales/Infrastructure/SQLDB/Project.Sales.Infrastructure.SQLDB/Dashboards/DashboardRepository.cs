using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.Dashboards;

namespace Project.Sales.Infrastructure.SQLDB.Dashboards
{
    public class DashboardRepository : IDashboardsRepository
    {
        private readonly ConnectionProvider provider;

        public DashboardRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }
        public async Task<IEnumerable<DashboardInfo>> GetCustomerTotal()
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT  COUNT(is_deleted) AS CustomerTotal
                FROM users
                WHERE is_deleted = 0
                GROUP BY is_deleted
            ";
            var result = await connect.QueryAsync<DashboardInfo>(sql);
            return result;
        }

        public async Task<IEnumerable<ProductTotalInfo>> GetProductTotal()
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT  COUNT(is_deleted) AS ProductTotal
                FROM product_details
                WHERE is_deleted = 0
                GROUP BY is_deleted
            ";
            var result = await connect.QueryAsync<ProductTotalInfo>(sql);
            return result;
        }

        public async Task<IEnumerable<OrderTotalInfo>> GetOrderTotal()
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT  COUNT(is_ordered) AS OrderTotal
                FROM orders
                WHERE is_ordered = 1
                GROUP BY is_ordered
            ";
            var result = await connect.QueryAsync<OrderTotalInfo>(sql);
            return result;
        }

        public async Task<IEnumerable<SoldOutProductInfo>> SoldOutProducts()
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT  COUNT(is_deleted) AS SoldOutProduct 
                FROM product_details
                WHERE is_deleted = 0 AND quantity<=5
                GROUP BY  is_deleted
            ";
            var result = await connect.QueryAsync<SoldOutProductInfo>(sql);
            return result;
        }

        public async Task<IEnumerable<OrderUnconfimred>> GetOrderUnconfimred()
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT order_code AS OrderCode,  
                full_name AS FullName, 
                order_total AS OrderTotal, 
                status from orders
                WHERE status =1
            ";
            var result = await connect.QueryAsync<OrderUnconfimred>(sql);
            return result;
        }
        public async Task<IEnumerable<NewCustomerInfo>> GetNewCustomer()
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT u.id AS Id, 
                c.first_name+''+c.last_name AS FullName, 
                c.birthday, 
                c.phone_number AS PhoneNumber,
                u.created_at AS CreateAt  
                FROM users AS u Left join customers AS c 
                ON c.user_id = u.id
                WHERE Convert(Date,u.created_at) >= GetDate() -30
            ";
            var result = await connect.QueryAsync<NewCustomerInfo>(sql);
            return result;
        }
    }
}
