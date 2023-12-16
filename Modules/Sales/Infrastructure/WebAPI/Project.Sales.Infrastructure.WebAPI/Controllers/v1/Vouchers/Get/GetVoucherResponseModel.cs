namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Get
{
    public class GetVoucherResponseModel
    {
        public IEnumerable<VoucherModel> Vouchers { get; set; }

        public GetVoucherResponseModel()
        {
            this.Vouchers = new List<VoucherModel>();
        }
    }
}
