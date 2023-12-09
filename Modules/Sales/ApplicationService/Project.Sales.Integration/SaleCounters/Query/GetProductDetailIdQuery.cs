using Project.Core.ApplicationService.Queries;

namespace Project.Sales.Integration.SaleCounters.Query
{
    public class GetProductDetailIdQuery : IQuery<GetProductDetailIdQueryResult>
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }

        public GetProductDetailIdQuery(int productId, int colorId, int sizeId)
        {
            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;
        }
    }
}
