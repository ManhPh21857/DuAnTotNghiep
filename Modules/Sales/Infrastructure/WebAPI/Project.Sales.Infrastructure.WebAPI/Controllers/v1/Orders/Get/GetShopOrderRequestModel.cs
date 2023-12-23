namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class GetShopOrderRequestModel
    {
        public string? Name { get; set; }
        public List<int>? ListStatus { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
