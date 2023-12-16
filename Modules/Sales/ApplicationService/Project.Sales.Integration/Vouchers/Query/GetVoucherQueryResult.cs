using Project.Sales.Domain.Vouchers;

namespace Project.Sales.Integration.Vouchers.Query
{
    public class GetVoucherQueryResult
    {
        public IEnumerable<Voucher> Vouchers { get; set; }

        public GetVoucherQueryResult(IEnumerable<Voucher> vouchers)
        {
            Vouchers = vouchers;
        }
    }
}
