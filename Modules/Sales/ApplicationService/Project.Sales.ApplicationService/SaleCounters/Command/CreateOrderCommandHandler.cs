using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Integration.SaleCounters.Command;

namespace Project.Sales.ApplicationService.SaleCounters.Command
{
    public class CreateOrderCommandHandler : CommandHandler<CreateOrderCommand, CreateOrderCommandResult>
    {
        private readonly ISaleCounterRepository saleCounterRepository;
        public CreateOrderCommandHandler(ISaleCounterRepository saleCounterRepository)
        {
            this.saleCounterRepository = saleCounterRepository;
        }

        public async override Task<CreateOrderCommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var createOder = new OrderDetailInfo
            {
                UserId = request.UserId,
                EmployeeId = request.EmployeeId,
                AddressId = request.AddressId,
                Total = request.Total
            };
            await this.saleCounterRepository.CreateOrder(createOder);
            return new CreateOrderCommandResult(true);
        }
    }
}