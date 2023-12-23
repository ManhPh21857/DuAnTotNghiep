namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class GetOrderDetailResponseModel
    {
        public IEnumerable<OrderDetailModel> OrderDetails { get; set; }

        public GetOrderDetailResponseModel()
        {
            this.OrderDetails = new List<OrderDetailModel>();
        }
    }
}
