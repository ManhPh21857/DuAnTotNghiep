using Project.Core.ApplicationService.Queries;

namespace Project.Sales.Integration.Vouchers.Query
{
    public class GetVoucherQuery : IQuery<GetVoucherQueryResult>
    {
        public float? TotalPrice { get; set; }
        public int? Page { get; set; }

        public GetVoucherQuery(float? totalPrice, int? page)
        {
            this.TotalPrice = totalPrice;
            this.Page = page;
        }
    }
}
