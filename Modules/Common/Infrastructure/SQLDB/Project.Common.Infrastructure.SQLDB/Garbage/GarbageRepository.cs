using Dapper;
using Project.Common.Domain.Garbage;
using Project.Core.Domain.Enums;
using Project.Core.Infrastructure.SQLDB.Providers;

namespace Project.Common.Infrastructure.SQLDB.Garbage
{
    public class GarbageRepository : IGarbageRepository
    {
        private readonly ConnectionProvider provider;

        public GarbageRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IEnumerable<int>> GetOrderNotYet()
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                id
                FROM
	                orders
                WHERE
	                payment_method_id = @PaymentMethodId
	                AND is_paid = 0
	                AND FORMAT(order_date, 'yyyy-MM-dd') < @OrderDate
            ";

            var result = await connect.QueryAsync<int>(query,
                new
                {
                    PaymentMethodId = PaymentMethod.MoMoPayment.GetHashCode(),
                    OrderDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")
                }
            );

            return result;
        }
    }
}
