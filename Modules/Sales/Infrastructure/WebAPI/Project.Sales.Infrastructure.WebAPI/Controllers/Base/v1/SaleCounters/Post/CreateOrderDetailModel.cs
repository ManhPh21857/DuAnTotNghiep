namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class CreateOrderDetailModel
    {
        public OrderModel Order { get; set; }
        public List<OrderDetailModel> Orderdetails { get; set; }

    }
}
