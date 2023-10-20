namespace Project.Product.Domain.Products;

public interface IProductRepository
{
    #region Product

    Task<IEnumerable<ProductView>> GetProducts(int skip, int take);
    Task<int> CreateProduct(ProductInfo param);
    Task DeleteProduct(int id);

    #endregion

    #region Product Color

    Task<int> CreateProductColor(int productId, int colorId, int imageId);
    Task DeleteProductColor(int productId);

    #endregion

    #region Product Size

    Task<int> CreateProductSize(int productId, int sizeId);
    Task DeleteProductSize(int productId);

    #endregion

    #region Product Detail

    Task CreateProductDetail(ProductDetailInfo param);
    Task DeleteProductDetail(int productId);

    #endregion
}