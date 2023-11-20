using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.SaleCounters;

namespace Project.Sales.Infrastructure.SQLDB.SaleCounters
{
    public class SaleCounterRepository : ISaleCounterRepository
    {
        private readonly ConnectionProvider provider;

        public SaleCounterRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }
        public async Task<SaleCounterInfo> GetSaleCounterView(int id)
        {
            await using var connect = await provider.Connect();

            const string query = @"
            SELECT
	            p.[Id]			 AS Id
               ,p.[Name]		 AS Name
               ,p.[Image]		 AS Image
               ,MIN(pd.[price])	 AS MinPrice
               ,MAX(pd.[price])	 AS MaxPrice
               ,SUM(pd.Quantity) AS Quantity
               ,p.[Description]	 AS Description
            FROM
	            [dbo].[products] AS p
	            LEFT JOIN [dbo].[product_details] AS pd
		            ON p.[Id] = pd.[product_id]
		            AND pd.[is_deleted] = 0
            WHERE
	            p.[is_deleted] = 0
                AND p.[id] = @Id
            GROUP BY
	            p.[Id]
               ,p.[Name]
               ,p.[Image]
               ,p.[Code]
               ,p.[Description]
            ORDER BY
	            p.[Code]
        ";

            var result = await connect.QueryFirstOrDefaultAsync<SaleCounterInfo>(query,
                new
                {
                    Id = id
                }
            );

            return result;
        }

        public async Task<SaleCounterResponse> GetSaleCounterView(int skip, int take)
        {
            await using var connect = await provider.Connect();

            const string query = @"
            SELECT
	            p.[Id]			 AS Id
               ,p.[Name]		 AS Name
               ,p.[Image]		 AS Image
               ,MIN(pd.[price])	 AS MinPrice
               ,MAX(pd.[price])	 AS MaxPrice
               ,SUM(pd.Quantity) AS Quantity
            FROM
	            [dbo].[products] AS p
	            LEFT JOIN [dbo].[product_details] AS pd
		            ON p.[Id] = pd.[product_id]
		            AND pd.[is_deleted] = 0
            WHERE
	            p.[is_deleted] = 0
            GROUP BY
	            p.[Id]
               ,p.[Name]
               ,p.[Image]
               ,p.[Code]
            ORDER BY
	            p.[Code]
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY

            SELECT
	            COUNT([id]) AS TotalProduct
            FROM
	            [dbo].[products]
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

            var result = new SaleCounterResponse
            {
                Salecounters = response.Read<SaleCounterInfo>(),
                TotalProduct = response.ReadFirstOrDefault<int>()
            };

            return result;
        }
    }
}
