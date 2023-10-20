using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Constants;
using Project.Product.Domain.Enums;
using Project.Product.Domain.Products;

namespace Project.Product.Infrastructure.SQLDB.Products;

public class ProductRepository : IProductRepository
{
    private readonly ConnectionProvider provider;

    public ProductRepository(ConnectionProvider provider)
    {
        this.provider = provider;
    }

    #region Product

    public async Task<IEnumerable<ProductView>> GetProducts(int skip, int take)
    {
        await using var connect = await provider.Connect();

        const string query = @"
            SELECT
	            p.[Id]		    AS Id
               ,p.[Code]	    AS Code
               ,p.[Name]	    AS Name
               ,p.[Image]       AS Image
               ,p.data_version  AS DataVersion
               ,c.[Name]	    AS ClassificationName
               ,m.[Name]	    AS MaterialName
               ,pd.Quantity     AS Quantity
               ,pd.AvgPrice     AS AvgPrice
            FROM
	            [dbo].[products] AS p
	            LEFT JOIN [dbo].[classifications] AS c
		            ON p.[classification_id] = c.[Id]
	            LEFT JOIN [dbo].[materials] AS m
		            ON p.[material_id] = m.[Id]
	            LEFT JOIN (
		            SELECT
			            [product_id]
		               ,SUM([price]) AS Quantity
		               ,AVG([price]) AS AvgPrice
		            FROM
			            [dbo].[product_details]
		            GROUP BY
			            [product_id]
	            ) AS pd
		            ON p.[Id] = pd.[product_id]
            WHERE
	            p.is_deleted = @IsDeleted
            ORDER BY
	            p.[Code]
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY
        ";

        var result = await connect.QueryAsync<ProductView>(query,
            new
            {
                IsDeleted = CommonConst.Valid,
                Skip = skip,
                Take = take
            }
        );

        return result;
    }

    public async Task<int> CreateProduct(ProductInfo param)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            INSERT [dbo].[products] (
	            [code]
               ,[name]
               ,[classification_id]
               ,[material_id]
               ,[supplier_id]
               ,[trademark_id]
               ,[origin_id]
               ,[description]
            )
            VALUES (
	            @Code
               ,@Name
               ,@ClassificationId
               ,@MaterialId
               ,@SupplierId
               ,@TrademarkId
               ,@OriginId
               ,@Description
            )
            SELECT @@IDENTITY AS Id
        ";

        var result = await connect.QueryFirstOrDefaultAsync<int>(command,
            new
            {
                Code = param.Code,
                Name = param.Name,
                ClassificationId = param.ClassificationId,
                MaterialId = param.MaterialId,
                SupplierId = param.SupplierId,
                TrademarkId = param.TrademarkId,
                OriginId = param.OriginId,
                Description = param.Description
            }
        );

        return result;
    }

    public async Task DeleteProduct(int id)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            UPDATE [dbo].[products]
            SET
	            [is_deleted] = @IsDeleted
            WHERE
	            [id] = @Id
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                IsDeleted = IsDeleted.Yes,
                Id = id
            }
        );

        result.IsOptimisticLocked();
    }

    #endregion

    #region Product Color

    public async Task<int> CreateProductColor(int productId, int colorId, int imageId)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            INSERT [dbo].[product_colors] (
	            [product_id]
               ,[color_id]
               ,[image_id]
            )
            VALUES (
	            @ProductId
               ,@ColorId
               ,@ImageId
            )
            SELECT @@IDENTITY 
        ";

        int result = await connect.QueryFirstOrDefaultAsync<int>(command,
            new
            {
                ProductId = productId,
                ColorId = colorId,
                ImageId = imageId
            }
        );

        return result;
    }

    public async Task DeleteProductColor(int productId)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_colors]
            SET
	            [is_deleted] = @IsDeleted
            WHERE
	            [product_id] = @ProductId
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                IsDeleted = IsDeleted.Yes,
                ProductId = productId
            }
        );

        result.IsOptimisticLocked();
    }

    #endregion

    #region Product Size

    public async Task<int> CreateProductSize(int productId, int sizeId)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            INSERT [dbo].[product_sizes] (
	            [product_id]
               ,[size_id]
            )
            VALUES (
	            @ProductId
               ,@SizeId
            )
            SELECT @@IDENTITY
        ";

        int result = await connect.QueryFirstOrDefaultAsync<int>(command,
            new
            {
                ProductId = productId,
                SizeId = sizeId
            }
        );

        return result;
    }

    public async Task DeleteProductSize(int productId)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_sizes]
            SET
	            [is_deleted] = @IsDeleted
            WHERE
	            [product_id] = @ProductId
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                IsDeleted = IsDeleted.Yes,
                ProductId = productId
            }
        );

        result.IsOptimisticLocked();
    }

    #endregion

    #region Product Detail

    public async Task CreateProductDetail(ProductDetailInfo param)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            INSERT [dbo].[product_details] (
	            [product_id]
               ,[product_color_id]
               ,[product_size_id]
               ,[import_price]
               ,[price]
               ,[quantity]
            )
            VALUES (
	            @ProductId
               ,@ProductColorId
               ,@ProductSizeId
               ,@ImportPrice
               ,@Price
               ,@Quantity
            )
        ";

        await connect.ExecuteAsync(command,
            new
            {
                ProductId = param.ProductId,
                ProductColorId = param.ColorId,
                ProductSizeId = param.SizeId,
                ImportPrice = param.ImportPrice,
                Price = param.Price,
                Quantity = param.Quantity
            }
        );
    }

    public async Task DeleteProductDetail(int productId)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
            SET
	            [is_deleted] = @IsDeleted
            WHERE
	            [product_id] = @ProductId
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                IsDeleted = IsDeleted.Yes,
                ProductId = productId
            }
        );

        result.IsOptimisticLocked();
    }

    #endregion
}