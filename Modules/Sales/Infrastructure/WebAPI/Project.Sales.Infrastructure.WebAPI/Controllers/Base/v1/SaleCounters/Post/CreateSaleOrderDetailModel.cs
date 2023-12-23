namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class CreateSaleOrderDetailModel
    {
        public OrderModel1 Order { get; set; }
        public List<OrderSaleDetailModel> Orderdetails { get; set; }

    }
}
