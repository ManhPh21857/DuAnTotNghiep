using Project.Core.ApplicationService.Commands;
using Project.Core.Domain.Enums;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Command;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class ReceivedOrderCommandHandler : CommandHandler<ReceivedOrderCommand, ReceivedOrderCommandResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public ReceivedOrderCommandHandler(
            IProductRepository productRepository,
            IOrderRepository orderRepository
        )
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public async override Task<ReceivedOrderCommandResult> Handle(
            ReceivedOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            var order = await this.orderRepository.GetOrder(request.Id);
            if (order.IsPaid == PayType.NotYet.GetHashCode())
            {
                var orderDetails = await this.orderRepository.GetOrderDetails(request.Id);
                foreach (var item in orderDetails)
                {
                    var productDetail = await this.productRepository.GetProductDetails(item.ProductId, item.ColorId, item.SizeId);

                    await this.productRepository.UpdateProductDetailActualQuantity(
                        productDetail.Id,
                        productDetail.ActualQuantity - item.Quantity
                    );
                }
            }

            await this.orderRepository.FinishOrderCashPayment(new FinishOrderOnlinePaymentParam
                {
                    Id = request.Id,
                    PaymentDate = DateTime.Now,
                    IsPaid = PayType.Paid.GetHashCode(),
                    Status = OrderStatus.Received.GetHashCode()
                }
            );

            return new ReceivedOrderCommandResult(true);
        }
    }
}
