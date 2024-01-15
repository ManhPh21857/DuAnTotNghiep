namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class OrderTotalModelResponseModel
    {
        public IEnumerable<OrderTotalModel> OrderTotals { get; set; }

        public OrderTotalModelResponseModel(IEnumerable<OrderTotalModel> orderTotals)
        {
            OrderTotals = orderTotals;
        }
    }
}
