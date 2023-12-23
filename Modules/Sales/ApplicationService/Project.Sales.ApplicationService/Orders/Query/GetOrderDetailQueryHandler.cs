using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Query;

namespace Project.Sales.ApplicationService.Orders.Query
{
    public class GetOrderDetailQueryHandler : QueryHandler<GetOrderDetailQuery, GetOrderDetailQueryResult>
    {
        private readonly IOrderRepository orderRepository;

        public GetOrderDetailQueryHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async override Task<GetOrderDetailQueryResult> Handle(
            GetOrderDetailQuery request,
            CancellationToken cancellationToken
        )
        {
            var orderDetails = await this.orderRepository.GetOrderDetails(request.OrderId);

            return new GetOrderDetailQueryResult(orderDetails);
        }
    }
}
