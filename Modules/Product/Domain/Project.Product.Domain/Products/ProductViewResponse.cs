namespace Project.Product.Domain.Products
{
    public class ProductViewResponse
    {
        public IEnumerable<ProductView> Products { get; set; }
        public int TotalProduct { get; set; }
    }
}
