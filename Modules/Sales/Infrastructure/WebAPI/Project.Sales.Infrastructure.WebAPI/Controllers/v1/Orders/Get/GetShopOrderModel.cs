namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class GetShopOrderModel
    {
        public int Id { get; set; }
        public Guid OrderCode { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public float MerchandiseSubtotal { get; set; }
        public float OrderTotal { get; set; }
        public int PaymentMethodId { get; set; }
        public int IsOrdered { get; set; }
        public int IsPaid { get; set; }
        public int Status { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
