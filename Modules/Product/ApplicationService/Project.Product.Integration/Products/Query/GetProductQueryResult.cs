using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductQueryResult
    {
        public List<ProductView> Products { get; set; }

        public GetProductQueryResult(List<ProductView> products)
        {
            this.Products = products;
        }
    }
}