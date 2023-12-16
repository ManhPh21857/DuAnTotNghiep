namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class GetOrderResponseModel
    {
        public IEnumerable<GetOrderModel> Orders { get; set; }

        public GetOrderResponseModel()
        {
            this.Orders = new List<GetOrderModel>();
        }
    }
}
