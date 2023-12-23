using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.HumanResources.Domain.Customers;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Query;

namespace Project.Sales.ApplicationService.Orders.Query
{
    public class GetOrderQueryHandler : QueryHandler<GetOrderQuery, GetOrderQueryResult>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;

        public GetOrderQueryHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            ISessionInfo sessionInfo
        )
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<GetOrderQueryResult> Handle(
            GetOrderQuery request,
            CancellationToken cancellationToken
        )
        {
            int userId = this.sessionInfo.UserId.Value;
            int? customerId = await this.customerRepository.GetCustomerId(userId);
            if (customerId is null)
            {
                throw new DomainException("", "Khách hàng không tồn tại");
            }

            var orders = await this.orderRepository.GetOrders(customerId.Value, null);
            foreach (var order in orders)
            {
                order.OrderDetails = await this.orderRepository.GetOrderDetails(order.Id);
            }

            return new GetOrderQueryResult(orders);
        }
    }
}
