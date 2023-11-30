using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Integration.SaleCounters.Command;

namespace Project.Sales.ApplicationService.SaleCounters.Command
{
    public class CreateOrderDetailCommandHandler : CommandHandler<CreateOrderDetailCommand, CreateOrderDetailCommandResult>
    {
        private readonly ISaleCounterRepository saleCounterRepository;
        public CreateOrderDetailCommandHandler(ISaleCounterRepository saleCounterRepository)
        {
            this.saleCounterRepository = saleCounterRepository;
        }

        public async override Task<CreateOrderDetailCommandResult> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var check = await this.saleCounterRepository.GetSaleCounterViewId(request.ProductId, request.ColorId, request.SizeId);
            if (check != null)
            {
               
                var createOderDetail = new OrderDetailInfo
                {
                    OrderId = request.OrderId,
                    VoucherId = request.VoucherId,
                    ProductDetailId = check.ProductDetailId,
                    Price = request.Price,
                    Quantity = request.Quantity
                };
              
                    await this.saleCounterRepository.CreateOrderDetail(createOderDetail);
              
            }
            else
            {
                throw new Exception();
            }
            return new CreateOrderDetailCommandResult(true);
        }
    }
}
