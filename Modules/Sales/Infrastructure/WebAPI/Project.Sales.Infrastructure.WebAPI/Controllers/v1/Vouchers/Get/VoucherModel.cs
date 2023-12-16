namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Get
{
    public class VoucherModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VoucherType { get; set; }
        public float MinimumPrice { get; set; }
        public float Discount { get; set; }
        public int DiscountType { get; set; }
        public float MaximumDiscount { get; set; }
        public DateTime ApplyPeriodStart { get; set; }
        public DateTime ApplyPeriodEnd { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
