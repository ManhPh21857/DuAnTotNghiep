using Project.Core.ApplicationService.Queries;

namespace Project.Sales.Integration.SaleCounters.Query
{
    public class GetSaleCounterIdQuery : IQuery<GetSaleCounterIdQueryResult>
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }

        public GetSaleCounterIdQuery(int productId, int colorId, int sizeId)
        {
            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;
        }
    }
}
