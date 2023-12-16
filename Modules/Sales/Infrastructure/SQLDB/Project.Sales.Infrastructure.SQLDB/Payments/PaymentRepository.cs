using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.Payments;

namespace Project.Sales.Infrastructure.SQLDB.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ConnectionProvider provider;

        public PaymentRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]		   AS Id
                   ,[Name]		   AS Name
                   ,[data_version] AS DataVersion
                FROM
	                [dbo].[payment_methods]
                WHERE
	                [is_deleted] = 0
            ";

            var result = await connect.QueryAsync<PaymentMethod>(query);

            return result;
        }
    }
}
