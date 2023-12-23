namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Get
{
    public class GetAllVoucherResponseModel
    {
        public IEnumerable<GetVoucherModel> Vouchers { get; set; }
        public int TotalPage { get; set; }

        public GetAllVoucherResponseModel()
        {
            this.Vouchers = new List<GetVoucherModel>();
        }
    }
}
