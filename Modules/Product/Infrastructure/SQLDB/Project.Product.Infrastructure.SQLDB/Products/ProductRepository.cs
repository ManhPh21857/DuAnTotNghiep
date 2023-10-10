using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Constants;
using Project.Product.Domain.Products;

namespace Project.Product.Infrastructure.SQLDB.Products;

public class ProductRepository : IProductRepository
{
    private readonly ConnectionProvider provider;

    public ProductRepository(ConnectionProvider provider)
    {
        this.provider = provider;
    }

    public async Task<List<ProductInfo>> GetProducts(int skip, int take)
    {
        await using var connect = await provider.Connect();

        const string query = @"
            SELECT
	            p.[Id]	 AS Id
               ,p.[Name] AS Name
               ,i.[Url]	 AS Url
               ,(
		            SELECT
			            MIN([price])
		            FROM
			            [product_details]
		            WHERE
			            [product_id] = p.Id
			            AND [is_deleted] = @IsDeleted
	            )
	            AS MinPrice
               ,(
		            SELECT
			            MAX([price])
		            FROM
			            [product_details]
		            WHERE
			            [product_id] = p.Id
			            AND [is_deleted] = @IsDeleted
	            )
	            AS MaxPrice
               ,CAST(ROUND((
		            SELECT
			            AVG(Evaluate)
		            FROM
			            [evaluates]
		            WHERE
			            product_id = p.Id
                        AND [is_deleted] = @IsDeleted
		            GROUP BY
			            product_id
	            ), 0) AS INT)
	            AS Evaluate
            FROM
	            products AS p
	            LEFT JOIN [images] AS i
		            ON p.image_id = i.Id
            WHERE
	            p.[is_deleted] = @IsDeleted
            ORDER BY
	            p.Id
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY";

        var result = (await connect.QueryAsync<ProductInfo>(query,
            new
            {
                IsDeleted = CommonConst.Valid,
                Skip = skip,
                Take = take
            })).ToList();

        return result;
    }
}