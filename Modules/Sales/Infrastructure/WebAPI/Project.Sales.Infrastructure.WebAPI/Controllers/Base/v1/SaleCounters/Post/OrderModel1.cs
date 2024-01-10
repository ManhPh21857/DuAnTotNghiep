namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class OrderModel1
    {

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public float VoucherApplied { get; set; }
        public float OrderTotal { get; set; }
        public int VoucherId { get; set; }
        public float MerchandiseSubtotal { get; set; }
        public int PaymentMethodId { get; set; }
        public int? Status { get; set; }

    }
}
