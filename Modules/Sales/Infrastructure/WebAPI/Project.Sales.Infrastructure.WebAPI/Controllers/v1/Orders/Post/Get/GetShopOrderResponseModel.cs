namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class GetShopOrderResponseModel
    {
        public IEnumerable<GetShopOrderModel> Orders { get; set; }
        public int TotalPage { get; set; }

        public GetShopOrderResponseModel()
        {
            this.Orders = new List<GetShopOrderModel>();
        }
    }
}
