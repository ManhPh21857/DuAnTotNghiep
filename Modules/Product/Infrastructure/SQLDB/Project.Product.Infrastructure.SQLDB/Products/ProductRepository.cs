using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Products;

namespace Project.Product.Infrastructure.SQLDB.Products;

public class ProductRepository : IProductRepository
{
    private readonly ConnectionProvider provider;

    public ProductRepository(ConnectionProvider provider)
    {
        this.provider = provider;
    }

    //public async Task<List<ProductInfo>> GetProducts(int skip, int take)
    //{
    //    await using var connect = await provider.Connect();

    //    const string query = @"
    //        SELECT
    //         p.[Id]	 AS Id
    //           ,p.[Name] AS Name
    //           ,i.[Url]	 AS Url
    //           ,(
    //          SELECT
    //           MIN([price])
    //          FROM
    //           [product_details]
    //          WHERE
    //           [product_id] = p.Id
    //           AND [is_deleted] = @IsDeleted
    //         )
    //         AS MinPrice
    //           ,(
    //          SELECT
    //           MAX([price])
    //          FROM
    //           [product_details]
    //          WHERE
    //           [product_id] = p.Id
    //           AND [is_deleted] = @IsDeleted
    //         )
    //         AS MaxPrice
    //           ,CAST(ROUND((
    //          SELECT
    //           AVG(Evaluate)
    //          FROM
    //           [evaluates]
    //          WHERE
    //           product_id = p.Id
    //                    AND [is_deleted] = @IsDeleted
    //          GROUP BY
    //           product_id
    //         ), 0) AS INT)
    //         AS Evaluate
    //        FROM
    //         products AS p
    //         LEFT JOIN [images] AS i
    //          ON p.image_id = i.Id
    //        WHERE
    //         p.[is_deleted] = @IsDeleted
    //        ORDER BY
    //         p.Id
    //        OFFSET @Skip ROWS
    //        FETCH NEXT @Take ROWS ONLY";

    //    var result = (await connect.QueryAsync<ProductInfo>(query,
    //        new
    //        {
    //            IsDeleted = CommonConst.Valid,
    //            Skip = skip,
    //            Take = take
    //        })).ToList();

    //    return result;
    //}

    #region Product

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

    #endregion
}