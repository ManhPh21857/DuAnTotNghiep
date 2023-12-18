using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Customers;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Command;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class CancelOrderCommandHandler : CommandHandler<CancelOrderCommand, CancelOrderCommandResult>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;

        public CancelOrderCommandHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            ISessionInfo sessionInfo
        )
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<CancelOrderCommandResult> Handle(
            CancelOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            int userId = this.sessionInfo.UserId.value;
            int? customerId = await this.customerRepository.GetCustomerId(userId);
            if (customerId is null)
            {
                throw new DomainException("", "Khách hàng không tồn tại");
            }

            var order = (await this.orderRepository.GetOrders(customerId.Value, request.OrderId)).FirstOrDefault();
            if (order is null)
            {
                throw new DomainException("", "Đơn hàng không tồn tại");
            }

            await this.orderRepository.CancelOrder(order.Id, customerId.Value);

            var result = new CancelOrderCommandResult(true, "");

            if (order.IsPaid == PayType.Paid.GetHashCode())
            {
                result.Message = "Hãy liên hệ shop để được hoàn tiền";
            }

            return result;
        }
    }
}
