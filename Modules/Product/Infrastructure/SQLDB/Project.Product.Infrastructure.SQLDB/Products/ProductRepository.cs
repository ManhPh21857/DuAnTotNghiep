using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Constants;
using Project.Product.Domain.Enums;
using Project.Product.Domain.Products;
using System.Drawing;

namespace Project.Product.Infrastructure.SQLDB.Products;

public class ProductRepository : IProductRepository
{
    private readonly ConnectionProvider provider;

    public ProductRepository(ConnectionProvider provider)
    {
        this.provider = provider;
    }

    #region Product

    public async Task<ProductView> GetProductView(int id)
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

        var result = await connect.QueryFirstOrDefaultAsync<ProductView>(query,
            new
            {
                Id = id
            }
        );

        return result;
    }

    public async Task<ProductViewResponse> GetProductView(int skip, int take)
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

        var result = new ProductViewResponse
        {
            Products = response.Read<ProductView>(),
            TotalProduct = response.ReadFirstOrDefault<int>()
        };
        
        return result;
    }

    public async Task<ProductInfo> GetProduct(int id)
    {
        await using var connect = await provider.Connect();

        const string query = @"
            SELECT
	            [Id]				AS Id
               ,[Code]				AS Code
               ,[supplier_id]		AS SupplierId
               ,[material_id]		AS MaterialId
               ,[classification_id] AS ClassificationId
               ,[origin_id]			AS OriginId
               ,[trademark_id]		AS TrademarkId
               ,[Name]				AS Name
               ,[Image]				AS Image
               ,[Description]		AS Description
               ,[data_version]		AS DataVersion
            FROM
	            [dbo].[products]
            WHERE
	            [Id] = @Id
	            AND [is_deleted] = @IsDeleted
        ";

        var result = await connect.QuerySingleOrDefaultAsync<ProductInfo>(query,
            new
            {
                Id = id,
                IsDeleted = IsDeleted.No
            }
        );

        return result;
    }

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
               ,[image]
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
               ,@Image
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
                Image = param.Image,
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

    public async Task UpdateProduct(ProductInfo param)
    {
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

    public async Task<IEnumerable<ProductColorInfo>> GetProductColor(int productId)
    {
        await using var connect = await provider.Connect();
        const string query = @"
            SELECT
	            pc.[color_id] AS ColorId
               ,pc.[Image]	  AS Image
               ,c.Color		  AS Color
            FROM
	            [dbo].[product_colors] AS pc
	            LEFT JOIN [dbo].[colors] AS c
		            ON pc.[color_id] = c.[id]
            WHERE
	            pc.[product_id] = @ProductId
	            AND pc.[is_deleted] = @IsDeleted
        ";

        var result = await connect.QueryAsync<ProductColorInfo>(query,
            new
            {
                ProductId = productId,
                IsDeleted = IsDeleted.No
            }
        );

        return result;
    }

    public async Task CreateProductColor(ProductColorInfo param)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            INSERT [dbo].[product_colors] (
	            [product_id]
               ,[color_id]
               ,[image]
            )
            VALUES (
	            @ProductId
               ,@ColorId
               ,@Image
            )
        ";

        await connect.ExecuteAsync(command,
            new
            {
                ProductId = param.ProductId,
                ColorId = param.ColorId,
                Image = param.Image
            }
        );
    }

    public async Task HardDeleteProductColor(int productId)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            DELETE FROM
	            [dbo].[product_colors]
            WHERE
	            [product_id] = @ProductId
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                ProductId = productId
            }
        );

        result.IsOptimisticLocked();
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

    public async Task<IEnumerable<ProductSizeInfo>> GetProductSize(int productId)
    {
        await using var connect = await provider.Connect();
        const string query = @"
            SELECT
	            ps.[size_id] AS SizeId
               ,s.Size		 AS Size
            FROM
	            [dbo].[product_sizes] AS ps
	            LEFT JOIN [dbo].[sizes] AS s
		            ON ps.[size_id] = s.[id]
            WHERE
	            ps.[product_id] = @ProductId
	            AND ps.[is_deleted] = @IsDeleted
        ";

        var result = await connect.QueryAsync<ProductSizeInfo>(query,
            new
            {
                ProductId = productId,
                IsDeleted = IsDeleted.No
            }
        );

        return result;
    }

    public async Task CreateProductSize(ProductSizeInfo param)
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
        ";

        await connect.ExecuteAsync(command,
            new
            {
                ProductId = param.ProductId,
                SizeId = param.SizeId
            }
        );
    }

    public async Task HardDeleteProductSize(int productId)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            DELETE FROM
	            [dbo].[product_sizes]
            WHERE
	            [product_id] = @ProductId
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                ProductId = productId
            }
        );

        result.IsOptimisticLocked();
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

    public async Task<IEnumerable<ProductDetailInfo>> GetProductDetail(int productId)
    {
        await using var connect = await provider.Connect();
        const string query = @"
            SELECT
	            [Id]		   AS Id
               ,[color_id]     AS ColorId
               ,[size_id]      AS SizeId
               ,[import_price] AS ImportPrice
               ,[Price]		   AS Price
               ,[Quantity]	   AS Quantity
               ,[data_version] AS DataVersion
            FROM
	            [dbo].[product_details]
            WHERE
	            [product_id] = @ProductId
	            AND [is_deleted] = @IsDeleted
        ";

        var result = await connect.QueryAsync<ProductDetailInfo>(query,
            new
            {
                ProductId = productId,
                IsDeleted = IsDeleted.No
            }
        );

        return result;
    }


    public async Task CreateProductDetail(ProductDetailInfo param)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            INSERT [dbo].[product_details] (
	            [product_id]
               ,[color_id]
               ,[size_id]
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

    public async Task UpdateProductDetail(ProductDetailInfo param)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
            SET
	            [import_price] = @ImportPrice
               ,[price]		   = @Price
               ,[quantity]	   = @Quantity
            WHERE
	            [id] = @Id
	            AND [product_id] = @ProductId
	            AND [color_id] = @ColorId
	            AND [size_id] = @SizeId
	            AND data_version = @DataVersion
	            AND is_deleted = @IsDeleted
        ";

        var result = await connect.ExecuteAsync(command,
            new
            {
                ImportPrice = param.ImportPrice,
                Price = param.Price,
                Quantity = param.Quantity,
                Id = param.Id,
                ProductId = param.ProductId,
                ColorId = param.ColorId,
                SizeId = param.SizeId,
                DataVersion = param.DataVersion,
                IsDeleted = IsDeleted.No
            }
        );

        result.IsOptimisticLocked();
    }

    public async Task DeleteProductDetailByProductId(int productId)
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

    public async Task DeleteProductDetail(int id)
    {
        await using var connect = await provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
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
}