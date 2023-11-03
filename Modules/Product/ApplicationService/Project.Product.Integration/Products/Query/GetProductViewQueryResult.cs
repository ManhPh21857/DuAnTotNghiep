using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductViewQueryResult
    {
        public IEnumerable<ProductView> Products { get; set; }
        public int TotalProduct { get; set; }

        public GetProductViewQueryResult(IEnumerable<ProductView> products, int totalProduct)
        {
            Products = products;
            TotalProduct = totalProduct;
        }
    }
}
