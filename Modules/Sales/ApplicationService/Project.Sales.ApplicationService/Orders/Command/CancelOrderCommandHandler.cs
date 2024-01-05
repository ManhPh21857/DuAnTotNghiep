using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Customers;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Command;
using System.Net;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class CancelOrderCommandHandler : CommandHandler<CancelOrderCommand, CancelOrderCommandResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;

        public CancelOrderCommandHandler(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            ISessionInfo sessionInfo
        )
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<CancelOrderCommandResult> Handle(
            CancelOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            var scope = TransactionFactory.Create();
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

            switch (order.Status)
            {
                case (int)OrderStatus.Pending:
                case (int)OrderStatus.NeedToConfirm:
                case (int)OrderStatus.Preparing:
                    await this.RefundProductQuantity(request.OrderId);
                    break;
                case (int)OrderStatus.Deliver:
                    await this.RefundProductBothQuantity(request.OrderId);
                    break;
                default:
                    throw new DomainException(
                        HttpStatusCode.BadRequest.GetHashCode().ToString(),
                        nameof(HttpStatusCode.BadRequest)
                    );
            }

            await this.orderRepository.CancelOrder(order.Id, customerId.Value);

            var result = new CancelOrderCommandResult(true, "");

            if (order.IsPaid == PayType.Paid.GetHashCode())
            {
                result.Message = "Hãy liên hệ shop để được hoàn tiền";
            }

            scope.Complete();
            return result;
        }

        private async Task RefundProductQuantity(int orderId)
        {
            var orderDetails = await this.orderRepository.GetOrderDetails(orderId);

            foreach (var item in orderDetails)
            {
                var productDetail =
                    await this.productRepository.GetProductDetails(item.ProductId, item.ColorId, item.SizeId);

                await this.productRepository.UpdateProductDetailQuantity(
                    productDetail.Id,
                    productDetail.Quantity + item.Quantity
                );
            }
        }

        private async Task RefundProductBothQuantity(int orderId)
        {
            var orderDetails = await this.orderRepository.GetOrderDetails(orderId);

            foreach (var item in orderDetails)
            {
                var productDetail =
                    await this.productRepository.GetProductDetails(item.ProductId, item.ColorId, item.SizeId);

                await this.productRepository.UpdateProductDetailBothQuantity(
                    productDetail.Id,
                    productDetail.Quantity + item.Quantity,
                    productDetail.ActualQuantity + item.Quantity
                );
            }
        }
    }
}
