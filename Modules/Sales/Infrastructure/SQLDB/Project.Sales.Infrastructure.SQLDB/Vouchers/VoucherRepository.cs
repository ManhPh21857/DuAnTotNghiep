using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.Vouchers;

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
                    AND [voucher_type] = 1
	                AND [apply_period_start] <= @DateNow
	                AND [apply_period_end] >= @DateNow
                    AND [minimum_price] <= @TotalPrice
                    AND [quantity] > 0
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

        public async Task<IEnumerable<Voucher>> GetAllVoucher(int skip, int take)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                v.[Id]								  AS Id
                   ,v.[Name]							  AS Name
                   ,v.[voucher_type]					  AS VoucherType
                   ,v.[minimum_price]					  AS MinimumPrice
                   ,v.[Discount]						  AS Discount
                   ,v.[discount_type]					  AS DiscountType
                   ,v.[maximum_discount]				  AS MaximumDiscount
                   ,v.[apply_period_start]				  AS ApplyPeriodStart
                   ,v.[apply_period_end]				  AS ApplyPeriodEnd
                   ,v.[Quantity]						  AS Quantity
                   ,CONCAT(elu.last_name, elu.first_name) AS CreatedBy
                   ,CONCAT(elu.last_name, elu.first_name) AS LastUpdateBy
                   ,v.[is_deleted]						  AS IsDeleted
                   ,v.[data_version]					  AS DataVersion
                FROM
	                [dbo].[vouchers] AS v
	                LEFT JOIN [dbo].[employees] AS ec
		                ON v.created_by = ec.Id
	                LEFT JOIN [dbo].[employees] AS elu
		                ON v.last_updated_by = elu.Id
                WHERE
	                v.[voucher_type] = 1
                ORDER BY
	                v.[Id]
                OFFSET @Skip ROWS
                FETCH NEXT @Take ROWS ONLY
            ";

            var result = await connect.QueryAsync<Voucher>(query,
                new
                {
                    Skip = skip,
                    Take = take
                }
            );

            return result;
        }

        public async Task<int> GetTotalPage()
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                count(id) AS Total
                FROM
	                vouchers
                WHERE
	                voucher_type = 1
            ";

            int result = await connect.QueryFirstOrDefaultAsync<int>(query);

            return result;
        }

        public async Task CreateVoucher(CreateVoucherParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                INSERT INTO [dbo].[vouchers] (
	                [name]
                   ,[voucher_type]
                   ,[minimum_price]
                   ,[discount]
                   ,[discount_type]
                   ,[maximum_discount]
                   ,[apply_period_start]
                   ,[apply_period_end]
                   ,[quantity]
                   ,[created_by]
                   ,[last_updated_by]
                )
                VALUES (
	                @Name
                   ,@VoucherType
                   ,@MinimumPrice
                   ,@Discount
                   ,@DiscountType
                   ,@MaximumDiscount
                   ,@ApplyPeriodStart
                   ,@ApplyPeriodEnd
                   ,@Quantity
                   ,@CreatedBy
                   ,@LastUpdatedBy
                )
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    Name = param.Name,
                    VoucherType = param.VoucherType,
                    MinimumPrice = param.MinimumPrice,
                    Discount = param.Discount,
                    DiscountType = param.DiscountType,
                    MaximumDiscount = param.MaximumDiscount,
                    ApplyPeriodStart = param.ApplyPeriodStart,
                    ApplyPeriodEnd = param.ApplyPeriodEnd,
                    Quantity = param.Quantity,
                    CreatedBy = param.CreatedBy,
                    LastUpdatedBy = param.LastUpdatedBy
                }
            );
        }

        public async Task UpdateVoucher(UpdateVoucherParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE [dbo].[vouchers]
                SET
	                [name]				 = @Name
                   ,[voucher_type]		 = @VoucherType
                   ,[minimum_price]		 = @MinimumPrice
                   ,[discount]			 = @Discount
                   ,[discount_type]		 = @DiscountType
                   ,[maximum_discount]	 = @MaximumDiscount
                   ,[apply_period_start] = @ApplyPeriodStart
                   ,[apply_period_end]	 = @ApplyPeriodEnd
                   ,[quantity]			 = @Quantity
                   ,[last_updated_by]	 = @LastUpdatedBy
                WHERE
	                [id] = @Id
	                AND data_version = @DataVersion
            ";

            var result = await connect.ExecuteAsync(command,
                new
                {
                    Id = param.Id,
                    Name = param.Name,
                    VoucherType = param.VoucherType,
                    MinimumPrice = param.MinimumPrice,
                    Discount = param.Discount,
                    DiscountType = param.DiscountType,
                    MaximumDiscount = param.MaximumDiscount,
                    ApplyPeriodStart = param.ApplyPeriodStart,
                    ApplyPeriodEnd = param.ApplyPeriodEnd,
                    Quantity = param.Quantity,
                    LastUpdatedBy = param.LastUpdatedBy,
                    DataVersion = param.DataVersion
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task DeleteVoucher(
            int id,
            int then,
            int now,
            byte[]? dataVersion,
            int lastUpdateBy
        )
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE vouchers
                SET
	                is_deleted		= @Now
                   ,last_updated_by = @LastUpdateBy
                WHERE
	                id = @Id
	                AND is_deleted = @Then
	                AND data_version = @DataVersion
            ";

            var result = await connect.ExecuteAsync(command,
                new
                {
                    Id = id,
                    Then = then,
                    Now = now,
                    DataVersion = dataVersion,
                    LastUpdateBy = lastUpdateBy
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<Voucher> GetVoucher(int id)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]				 AS Id
                   ,[Name]				 AS Name
                   ,[Quantity]			 AS Quantity
                   ,[voucher_type]		 AS VoucherType
                   ,[minimum_price]		 AS MinimumPrice
                   ,[Discount]			 AS Discount
                   ,[discount_type]		 AS DiscountType
                   ,[maximum_discount]	 AS MaximumDiscount
                   ,[apply_period_start] AS ApplyPeriodStart
                   ,[apply_period_end]	 AS ApplyPeriodEnd
                   ,[data_version]		 AS DataVersion
                FROM
	                vouchers
                WHERE
	                Id = @Id
            ";

            var result = await connect.QueryFirstOrDefaultAsync<Voucher>(query,
                new
                {
                    Id = id
                }
            );

            return result;
        }

        public async Task UpdateVoucherQuantity(int id, int quantity)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE vouchers
                SET
	                quantity = @Quantity
                WHERE
	                id = @Id
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    Id = id,
                    Quantity = quantity
                }
            );
        }
    }
}
