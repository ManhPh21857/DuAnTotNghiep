using Project.Core.ApplicationService.Queries;

namespace Project.Sales.Integration.Vouchers.Query
{
    public class GetVoucherQuery : IQuery<GetVoucherQueryResult>
    {
        public float TotalPrice { get; set; }

        public GetVoucherQuery(float totalPrice)
        {
            TotalPrice = totalPrice;
        }
    }
}
