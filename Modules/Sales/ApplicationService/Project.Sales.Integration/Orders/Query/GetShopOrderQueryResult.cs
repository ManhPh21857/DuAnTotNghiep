using Project.Sales.Domain.Orders;

namespace Project.Sales.Integration.Orders.Query
{
    public class GetShopOrderQueryResult
    {
        public IEnumerable<OrderInfo> Orders { get; set; }
        public int TotalPage { get; set; }

        public GetShopOrderQueryResult(IEnumerable<OrderInfo> orders, int totalPage)
        {
            this.Orders = orders;
            this.TotalPage = totalPage;
        }
    }
}
