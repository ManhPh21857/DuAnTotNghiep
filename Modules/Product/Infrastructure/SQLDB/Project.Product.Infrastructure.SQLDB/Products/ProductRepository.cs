using Dapper;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
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

    public async Task<ProductView> GetProductView(int id)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            SELECT
	            p.[Id]			        AS Id
               ,p.[Name]		        AS Name
               ,p.[Image]		        AS Image
               ,MIN(pd.[price])	        AS MinPrice
               ,MAX(pd.[price])	        AS MaxPrice
               ,SUM(pd.Quantity)        AS Quantity
               ,SUM(pd.actual_quantity) AS ActualQuantity
               ,p.[Description]	        AS Description
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

    public async Task<ProductViewResponse> GetProductView(int skip, int take, GetProductFilterParam filterParam)
    {
        await using var connect = await this.provider.Connect();

        var builder = new SqlBuilder();

        const string query = @"
            SELECT
	            p.[Id]			 AS Id
               ,p.[Name]		 AS Name
               ,p.[Image]		 AS Image
               ,(
		            SELECT
			            MIN(price)
		            FROM
			            product_details
		            WHERE
			            product_id = p.Id
	            )				 
	            AS MinPrice
               ,(
		            SELECT
			            MAX(price)
		            FROM
			            product_details
		            WHERE
			            product_id = p.Id
	            )				 
	            AS MaxPrice
               ,(
		            SELECT
			            SUM(Quantity)
		            FROM
			            product_details
		            WHERE
			            product_id = p.Id
	            )				 
	            AS Quantity
            FROM
	            [dbo].[products] AS p
	            LEFT JOIN [dbo].[product_details] AS pd
		            ON p.[Id] = pd.[product_id]
		            AND pd.[is_deleted] = 0
            /**where**/
            GROUP BY
	            p.[Id]
               ,p.[Name]
               ,p.[Image]
               ,p.[Code]
               ,p.[created_at]
            /**having**/
            ORDER BY
	            p.[created_at] DESC
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY;

            SELECT
	            COUNT(a.Id) AS TotalProduct
            FROM
	            (
		            SELECT
			            p.[Id] AS Id
		            FROM
			            [dbo].[products] AS p
			            LEFT JOIN [dbo].[product_details] AS pd
				            ON p.[Id] = pd.[product_id]
				            AND pd.[is_deleted] = 0
		            /**where**/
		            GROUP BY
			            p.[Id]
		               ,p.[Name]
		               ,p.[Image]
		               ,p.[Code]
		            /**having**/
	            ) AS a
        ";

        var template = builder.AddTemplate(query);

        builder.Where("p.[is_deleted] = 0",
            new
            {
                Skip = skip,
                Take = take
            }
        );

        if (!filterParam.ColorIds.IsNullOrEmpty())
        {
            builder.Where("pd.[color_id] IN @ColorIds", new { ColorIds = filterParam.ColorIds });
        }

        if (!filterParam.SizeIds.IsNullOrEmpty())
        {
            builder.Where("pd.[size_id] IN @SizeIds", new { SizeIds = filterParam.SizeIds });
        }

        if (!filterParam.SupplierIds.IsNullOrEmpty())
        {
            builder.Where("p.[supplier_id] IN @SupplierIds", new { SupplierIds = filterParam.SupplierIds });
        }

        if (!filterParam.MaterialIds.IsNullOrEmpty())
        {
            builder.Where("p.[material_id] IN @MaterialIds", new { MaterialIds = filterParam.MaterialIds });
        }

        if (!filterParam.ClassificationIds.IsNullOrEmpty())
        {
            builder.Where("p.[classification_id] IN @ClassificationIds",
                new { ClassificationIds = filterParam.ClassificationIds });
        }

        if (!filterParam.OriginIds.IsNullOrEmpty())
        {
            builder.Where("p.[origin_id] IN @OriginIds", new { OriginIds = filterParam.OriginIds });
        }

        if (!filterParam.TrademarkIds.IsNullOrEmpty())
        {
            builder.Where("p.[trademark_id] IN @TrademarkIds", new { TrademarkIds = filterParam.TrademarkIds });
        }

        if (!filterParam.Name.IsNullOrEmpty())
        {
            builder.Where("p.[name] LIKE @Name", new { Name = $"%{filterParam.Name}%" });
        }

        builder.Having("SUM(pd.Quantity) > 0");

        if (filterParam.MinPrice.HasValue)
        {
            builder.Having("MIN(pd.[price]) >= @MinPrice", new { MinPrice = filterParam.MinPrice });
        }

        if (filterParam.MaxPrice.HasValue)
        {
            builder.Having("MIN(pd.[price]) <= @MaxPrice", new { MaxPrice = filterParam.MaxPrice });
        }

        var response = await connect.QueryMultipleAsync(template.RawSql, template.Parameters);

        var result = new ProductViewResponse
        {
            Products = response.Read<ProductView>(),
            TotalProduct = response.ReadFirstOrDefault<int>()
        };

        return result;
    }

    public async Task<ProductInfo> GetProduct(int id)
    {
        await using var connect = await this.provider.Connect();

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

    public async Task<ProductViewResponse> GetProducts(int skip, int take)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            SELECT
	            p.[Id]		      AS Id
               ,p.[Code]	      AS Code
               ,p.[Name]	      AS Name
               ,p.[Image]         AS Image
               ,p.data_version    AS DataVersion
               ,c.[Name]	      AS ClassificationName
               ,m.[Name]	      AS MaterialName
               ,pd.Quantity       AS Quantity
               ,pd.ActualQuantity AS ActualQuantity
               ,pd.AvgPrice       AS AvgPrice
            FROM
	            [dbo].[products] AS p
	            LEFT JOIN [dbo].[classifications] AS c
		            ON p.[classification_id] = c.[Id]
	            LEFT JOIN [dbo].[materials] AS m
		            ON p.[material_id] = m.[Id]
	            LEFT JOIN (
		            SELECT
			            [product_id]
		               ,SUM([quantity])        AS Quantity
                       ,SUM([actual_quantity]) AS ActualQuantity
		               ,AVG([price])           AS AvgPrice
		            FROM
			            [dbo].[product_details]
		            GROUP BY
			            [product_id]
	            ) AS pd
		            ON p.[Id] = pd.[product_id]
            WHERE
	            p.is_deleted = 0
            ORDER BY
	            p.[created_at] DESC
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY;

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

    public async Task<int> CreateProduct(ProductInfo param)
    {
        await using var connect = await this.provider.Connect();
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
        await using var connect = await this.provider.Connect();

        const string command = @"
            UPDATE [dbo].[products]
            SET
	            [code]				= @Code
               ,[supplier_id]		= @SupplierId
               ,[material_id]		= @MaterialId
               ,[classification_id] = @ClassificationId
               ,[origin_id]			= @OriginId
               ,[trademark_id]		= @TrademarkId
               ,[name]				= @Name
               ,[image]				= @Image
               ,[description]		= @Description
            WHERE
	            id = @Id
	            AND data_version = @DataVersion
                AND is_deleted = 0
        ";

        var result = await connect.ExecuteAsync(command,
            new
            {
                Id = param.Id,
                Code = param.Code,
                SupplierId = param.SupplierId,
                MaterialId = param.MaterialId,
                ClassificationId = param.ClassificationId,
                OriginId = param.OriginId,
                TrademarkId = param.TrademarkId,
                Name = param.Name,
                Image = param.Image,
                Description = param.Description,
                DataVersion = param.DataVersion
            }
        );

        result.IsOptimisticLocked();
    }

    public async Task DeleteProduct(DeleteProductParam param)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[products]
            SET
	            [is_deleted] = 1
            WHERE
	            [id] = @Id
                AND [data_version] = @DataVersion
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                Id = param.Id,
                DataVersion = param.DataVersion
            }
        );

        result.IsOptimisticLocked();
    }

    public async Task<IEnumerable<int>> CheckProductOrder(int productId)
    {
        await using var connect = await this.provider.Connect();

        const string query = @"
            SELECT
	            o.Id AS Id
            FROM
	            orders AS o
	            LEFT JOIN order_details AS od
		            ON o.Id = od.order_id
            WHERE
	            o.order_date > @OrderDate
	            AND o.is_ordered = 1
	            AND o.status IN (0, 1, 2, 3)
	            AND od.product_id = @ProductId
        ";

        var result = await connect.QueryAsync<int>(query,
            new
            {
                OrderDate = DateTime.Now.AddDays(-15).ToString("d"),
                ProductId = productId
            }
        );

        return result;
    }

    #endregion

    #region Product Color

    public async Task<IEnumerable<ProductColorInfo>> GetProductColor(int productId)
    {
        await using var connect = await this.provider.Connect();
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
        await using var connect = await this.provider.Connect();
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
        await using var connect = await this.provider.Connect();
        const string command = @"
            DELETE FROM
	            [dbo].[product_colors]
            WHERE
	            [product_id] = @ProductId
        ";

        await connect.ExecuteAsync(command,
            new
            {
                ProductId = productId
            }
        );
    }

    public async Task DeleteProductColor(int productId)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_colors]
            SET
	            [is_deleted] = 1
            WHERE
	            [product_id] = @ProductId
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                ProductId = productId
            }
        );
    }

    #endregion

    #region Product Size

    public async Task<IEnumerable<ProductSizeInfo>> GetProductSize(int productId)
    {
        await using var connect = await this.provider.Connect();
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
        await using var connect = await this.provider.Connect();
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
        await using var connect = await this.provider.Connect();
        const string command = @"
            DELETE FROM
	            [dbo].[product_sizes]
            WHERE
	            [product_id] = @ProductId
        ";

        await connect.ExecuteAsync(command,
            new
            {
                ProductId = productId
            }
        );
    }

    public async Task DeleteProductSize(int productId)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_sizes]
            SET
	            [is_deleted] = 1
            WHERE
	            [product_id] = @ProductId
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                ProductId = productId
            }
        );
    }

    #endregion

    #region Product Detail

    public async Task<ProductDetailInfo?> GetProductDetailById(int id)
    {
        await using var connect = await this.provider.Connect();
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
	            [id] = @Id
	            AND [is_deleted] = 0
        ";

        var result = await connect.QueryFirstOrDefaultAsync<ProductDetailInfo>(query,
            new
            {
                Id = id
            }
        );

        return result;
    }

    public async Task<ProductDetailInfo> GetProductDetails(int productId, int colorId, int sizeId)
    {
        await using var connect = await this.provider.Connect();
        const string query = @"
            SELECT
	            [Id]		      AS Id
               ,[color_id]	      AS ColorId
               ,[size_id]	      AS SizeId
               ,[import_price]    AS ImportPrice
               ,[Price]		      AS Price
               ,[Quantity]	      AS Quantity
               ,[actual_quantity] As ActualQuantity
               ,[data_version]    AS DataVersion
            FROM
	            [dbo].[product_details]
            WHERE
	            [product_id] = @ProductId
	            AND [color_id] = @ColorId
	            AND [size_id] = @SizeId
	            AND [is_deleted] = 0
        ";

        var result = await connect.QueryFirstOrDefaultAsync<ProductDetailInfo>(query,
            new
            {
                ProductId = productId,
                ColorId = colorId,
                SizeId = sizeId
            }
        );

        return result;
    }

    public async Task<IEnumerable<ProductDetailInfo>> GetProductDetail(int productId)
    {
        await using var connect = await this.provider.Connect();
        const string query = @"
            SELECT
	            [Id]		      AS Id
               ,[color_id]        AS ColorId
               ,[size_id]         AS SizeId
               ,[import_price]    AS ImportPrice
               ,[Price]		      AS Price
               ,[Quantity]	      AS Quantity
               ,[actual_quantity] AS ActualQuantity
               ,[data_version]    AS DataVersion
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
        await using var connect = await this.provider.Connect();
        const string command = @"
            INSERT [dbo].[product_details] (
	            [product_id]
               ,[color_id]
               ,[size_id]
               ,[import_price]
               ,[price]
               ,[quantity]
               ,[actual_quantity]
            )
            VALUES (
	            @ProductId
               ,@ProductColorId
               ,@ProductSizeId
               ,@ImportPrice
               ,@Price
               ,@Quantity
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
                Quantity = param.ImportQuantity
            }
        );
    }

    public async Task UpdateProductDetail(ProductDetailInfo param)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
            SET
	            [import_price]    = @ImportPrice
               ,[price]		      = @Price
               ,[quantity]	      = @Quantity
               ,[actual_quantity] = @ActualQuantity
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
                ActualQuantity = param.ActualQuantity,
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
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
            SET
	            [is_deleted] = 1
            WHERE
	            [product_id] = @ProductId
        ";

        await connect.ExecuteAsync(command,
            new
            {
                ProductId = productId
            }
        );
    }

    public async Task DeleteProductDetail(int id)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
            SET
	            [is_deleted] = 1
            WHERE
	            [id] = @Id
        ";

        await connect.ExecuteAsync(command,
            new
            {
                IsDeleted = IsDeleted.Yes,
                Id = id
            }
        );
    }

    public async Task UpdateProductDetailQuantity(int id, int quantity)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
            SET
	            [quantity] = @Quantity
            WHERE
	            [id] = @Id
	            AND is_deleted = 0
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                Quantity = quantity,
                Id = id,
            }
        );

        result.IsOptimisticLocked();
    }

    public async Task UpdateProductDetailActualQuantity(int id, int actualQuantity)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
            SET
	            [actual_quantity] = @ActualQuantity
            WHERE
	            [id] = @Id
	            AND is_deleted = 0
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                ActualQuantity = actualQuantity,
                Id = id,
            }
        );

        result.IsOptimisticLocked();
    }

    public async Task UpdateProductDetailBothQuantity(int id, int quantity, int actualQuantity)
    {
        await using var connect = await this.provider.Connect();
        const string command = @"
            UPDATE [dbo].[product_details]
            SET
                [quantity] = @Quantity
	           ,[actual_quantity] = @ActualQuantity
            WHERE
	            [id] = @Id
	            AND is_deleted = 0
        ";

        int result = await connect.ExecuteAsync(command,
            new
            {
                Quantity = quantity,
                ActualQuantity = actualQuantity,
                Id = id,
            }
        );

        result.IsOptimisticLocked();
    }

    #endregion
}
