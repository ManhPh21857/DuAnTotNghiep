namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Get
{
    public class GetVoucherModel : VoucherModel
    {
        public int Quantity { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdateBy { get; set; }
        public int IsDeleted { get; set; }
    }
}
