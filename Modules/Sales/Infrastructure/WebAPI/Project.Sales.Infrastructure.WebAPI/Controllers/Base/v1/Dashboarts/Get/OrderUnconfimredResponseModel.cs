namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class OrderUnconfimredResponseModel
    {
        public IEnumerable<OrderUnconfimredModel> OrderUnConfimreds { get; set; }

        public OrderUnconfimredResponseModel(IEnumerable<OrderUnconfimredModel> orderUnConfimreds)
        {
            OrderUnConfimreds = orderUnConfimreds;
        }
    }
}
