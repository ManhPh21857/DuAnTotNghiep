using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Command;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class AssignOrderCommandHandler : CommandHandler<AssignOrderCommand, AssignOrderCommandResult>
    {
        private readonly IOrderRepository orderRepository;

        public AssignOrderCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async override Task<AssignOrderCommandResult> Handle(
            AssignOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            await this.orderRepository.AssignEmployee(request.Id, request.EmployeeId, request.DataVersion);

            return new AssignOrderCommandResult(true);
        }
    }
}
