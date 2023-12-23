namespace Project.Sales.Domain.Vouchers
{
    public class Voucher
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
        public int Quantity { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdateBy { get; set; }
        public int IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
