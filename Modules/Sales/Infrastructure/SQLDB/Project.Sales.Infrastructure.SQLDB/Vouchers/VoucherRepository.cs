using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.Vouchers;
using System.Data;

namespace Project.Sales.Infrastructure.SQLDB.Vouchers
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ConnectionProvider provider;

        public VoucherRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IEnumerable<Voucher>> GetVoucher(float totalPrice)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]				 AS Id
                   ,[Name]				 AS Name
                   ,[voucher_type]		 AS VoucherType
                   ,[minimum_price]		 AS MinimumPrice
                   ,[Discount]			 AS Discount
                   ,[discount_type]		 AS DiscountType
                   ,[maximum_discount]	 AS MaximumDiscount
                   ,[apply_period_start] AS ApplyPeriodStart
                   ,[apply_period_end]	 AS ApplyPeriodEnd
                   ,[data_version]		 AS DataVersion
                FROM
	                [dbo].[vouchers]
                WHERE
	                [is_deleted] = 0
	                AND [apply_period_start] <= @DateNow
	                AND [apply_period_end] >= @DateNow
                    AND [minimum_price] <= @TotalPrice
            ";

            var result = await connect.QueryAsync<Voucher>(query,
                new
                {
                    DateNow = DateTime.Now.ToString("d"),
                    TotalPrice = totalPrice
                }
            );

            return result;
        }
    }
}
