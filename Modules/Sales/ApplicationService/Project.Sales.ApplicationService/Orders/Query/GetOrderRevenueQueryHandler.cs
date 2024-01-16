using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Query;

namespace Project.Sales.ApplicationService.Orders.Query
{
    public class GetOrderRevenueQueryHandler : QueryHandler<GetOrderRevenueQuery, GetOrderRevenueQueryResult>
    {
        private readonly IOrderRepository orderRepository;

        public GetOrderRevenueQueryHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async override Task<GetOrderRevenueQueryResult> Handle(
            GetOrderRevenueQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await this.orderRepository.GetOrderRevenue();
            return new GetOrderRevenueQueryResult(result);
        }
    }
}
