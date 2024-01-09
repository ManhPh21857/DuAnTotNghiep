using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Query
{
    public class GetNewProductQueryResult
    {
        public IEnumerable<ProductView> Products { get; set; }

        public GetNewProductQueryResult(IEnumerable<ProductView> products)
        {
            this.Products = products;
        }
    }
}
