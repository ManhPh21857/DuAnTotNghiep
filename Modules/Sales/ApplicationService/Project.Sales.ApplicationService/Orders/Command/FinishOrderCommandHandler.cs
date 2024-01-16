using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Payments.Command;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class FinishOrderCommandHandler : CommandHandler<FinishOrderCommand, FinishOrderCommandResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMemoryCache memoryCache;
        private readonly IConfiguration configuration;

        public FinishOrderCommandHandler(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IMemoryCache memoryCache,
            IConfiguration configuration
        )
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.memoryCache = memoryCache;
            this.configuration = configuration;
        }

        public async override Task<FinishOrderCommandResult> Handle(
            FinishOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            this.memoryCache.TryGetValue("requestId", out string requestId);
            this.memoryCache.TryGetValue("orderId", out string orderId);
            string accessKey = this.configuration["MoMoPayment:AccessKey"];

            if (request.RequestId != requestId || request.OrderId != orderId || request.AccessKey != accessKey)
            {
                throw new DomainException(
                    HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    nameof(HttpStatusCode.BadRequest)
                );
            }

            var orderDetails = await this.orderRepository.GetOrderDetails(request.Id);
            foreach (var item in orderDetails)
            {
                var productDetail =
                    await this.productRepository.GetProductDetails(item.ProductId, item.ColorId, item.SizeId, null);

                await this.productRepository.UpdateProductDetailActualQuantity(
                    productDetail.Id,
                    productDetail.ActualQuantity - item.Quantity
                );
            }

            await this.orderRepository.FinishOrderOnlinePayment(new FinishOrderOnlinePaymentParam
                {
                    Id = request.Id,
                    OrderDate = DateTime.Now,
                    PaymentDate = DateTime.Now,
                    IsOrdered = OrderType.Ordered.GetHashCode(),
                    IsPaid = PayType.Paid.GetHashCode(),
                    Status = OrderStatus.NeedToConfirm.GetHashCode()
                }
            );

            return new FinishOrderCommandResult(true);
        }
    }
}
