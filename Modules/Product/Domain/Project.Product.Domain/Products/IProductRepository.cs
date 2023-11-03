namespace Project.Product.Domain.Products;

public interface IProductRepository
{
    #region Product

    Task<ProductViewResponse> GetProductView(int skip, int take);
    Task<ProductView> GetProductView(int id);
    Task<ProductInfo> GetProduct(int id);
    Task<IEnumerable<ProductView>> GetProducts(int skip, int take);
    Task<int> CreateProduct(ProductInfo param);
    Task UpdateProduct(ProductInfo param);
    Task DeleteProduct(int id);

    #endregion

    #region Product Color

    Task<IEnumerable<ProductColorInfo>> GetProductColor(int productId);
    Task CreateProductColor(ProductColorInfo param);
    Task HardDeleteProductColor(int productId);
    Task DeleteProductColor(int productId);

    #endregion

    #region Product Size

    Task<IEnumerable<ProductSizeInfo>> GetProductSize(int productId);
    Task CreateProductSize(ProductSizeInfo param);
    Task HardDeleteProductSize(int productId);
    Task DeleteProductSize(int productId);

    #endregion

    #region Product Detail

    Task<IEnumerable<ProductDetailInfo>> GetProductDetail(int productId);
    Task CreateProductDetail(ProductDetailInfo param);
    Task UpdateProductDetail(ProductDetailInfo param);
    Task DeleteProductDetailByProductId(int productId);
    Task DeleteProductDetail(int id);

    #endregion
}