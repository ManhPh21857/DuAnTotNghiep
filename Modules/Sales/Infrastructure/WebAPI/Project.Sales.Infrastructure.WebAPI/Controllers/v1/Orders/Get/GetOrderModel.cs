namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class GetOrderModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Guid OrderCode { get; set; }
        public float OrderTotal { get; set; }
        public int PaymentMethodId { get; set; }
        public int IsOrdered { get; set; }
        public int IsPaid { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public IEnumerable<OrderDetailModal> OrderDetails { get; set; }

        public GetOrderModel()
        {
            this.OrderDetails = new List<OrderDetailModal>();
        }
    }
}
