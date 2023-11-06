using Project.Product.Domain.Products;
using Project.Sales.Domain.CartDetails;

namespace Project.Sales.Integration.CartDetails.Query
{
    public class GetCartdetailQueryResult 
    {
        public IEnumerable<CartDetailInfo> Cartdetails { get; set; }
        public IEnumerable<ProductColorInfo> ProductColors { get; set; }
        public IEnumerable<ProductSizeInfo> ProductSizes { get; set; }

        public GetCartdetailQueryResult(
            IEnumerable<CartDetailInfo> cartdetails, 
            IEnumerable<ProductColorInfo> productColors, 
            IEnumerable<ProductSizeInfo> productSizes
        )
        {
            Cartdetails = cartdetails;
            ProductColors = productColors;
            ProductSizes = productSizes;
        }
    }
}
