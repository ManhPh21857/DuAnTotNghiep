using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Integration.SaleCounters.Command;

namespace Project.Sales.ApplicationService.SaleCounters.Command
{
    public class CreateOrderDetailCommandHandler : CommandHandler<CreateOrderDetailCommand, CreateOrderDetailCommandResult>
    {
        private readonly ISaleCounterRepository saleCounterRepository;
        private readonly ISessionInfo sessionInfo;
        public CreateOrderDetailCommandHandler(ISaleCounterRepository saleCounterRepository, ISessionInfo sessionInfo)
        {
            this.saleCounterRepository = saleCounterRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<CreateOrderDetailCommandResult> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var createOder = request.Order;
            var id = await this.saleCounterRepository.CreateOrder(createOder);
            foreach (var item in request.Orderdetails)
            {
                item.OrderId = id;
                await this.saleCounterRepository.CreateOrderDetail(item);
            }
            return new CreateOrderDetailCommandResult(true);
        }
    }
}
