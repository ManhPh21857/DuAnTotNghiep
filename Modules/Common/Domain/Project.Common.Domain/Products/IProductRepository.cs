namespace Project.Common.Domain.Products;

public interface IProductRepository
{
    Task<List<ProductInfo>> GetProducts(int skip, int take);
}