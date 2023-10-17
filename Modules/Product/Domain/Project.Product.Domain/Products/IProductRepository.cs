namespace Project.Product.Domain.Products;

public interface IProductRepository
{
    //Task<List<ProductInfo>> GetProducts(int skip, int take);

    #region Product

    Task<int> CreateProduct(ProductInfo param);

    #endregion

    #region Product Color

    Task<int> CreateProductColor(int productId, int colorId, int imageId);

    #endregion

    #region Product Size

    Task<int> CreateProductSize(int productId, int sizeId);

    #endregion

    #region Product Detail

    Task CreateProductDetail(ProductDetailInfo param);

    #endregion
}