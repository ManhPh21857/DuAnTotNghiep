using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductQueryResult
    {
        public IEnumerable<ProductView> Products { get; set; }
        public int TotalPage { get; set; }

        public GetProductQueryResult(IEnumerable<ProductView> products, int totalPage)
        {
            this.Products = products;
            this.TotalPage = totalPage;
        }
    }
}