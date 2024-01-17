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
                FROM customers
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
                SELECT c.id AS Id, 
                u.user_name AS UserName, 
                c.address, 
                c.phone_number AS PhoneNumber,
                c.created_at AS CreateAt  
                FROM customers AS c LEFT JOIN users AS u ON u.id= c.user_id
                WHERE Convert(Date,c.created_at) >= GetDate() -30
            ";
            var result = await connect.QueryAsync<NewCustomerInfo>(sql);
            return result;
        }

        public async Task<IEnumerable<SoldOutProductDetailInfo>> GetSoldOutProductDetail()
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT  p.name, c.color, s.size, pd.quantity, 
                pd.actual_quantity AS ActualQuantity, pd.price
                FROM product_details AS pd 
				LEFT JOIN products AS p 
				ON pd.product_id = p.id
				LEFT JOIN colors AS c ON pd.color_id = c.id
				LEFT JOIN sizes AS s ON pd.size_id = s.id
                WHERE pd.is_deleted = 0 AND pd.quantity<=5
            ";
            var result = await connect.QueryAsync<SoldOutProductDetailInfo>(sql);
            return result;
        }
    }
}
