using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Customers;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Command;
using System.Net;
using Project.Sales.Domain.Vouchers;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class CancelOrderCommandHandler : CommandHandler<CancelOrderCommand, CancelOrderCommandResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;
        private readonly IVoucherRepository voucherRepository;

        public CancelOrderCommandHandler(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            ISessionInfo sessionInfo,
            IVoucherRepository voucherRepository
        )
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
            this.voucherRepository = voucherRepository;
        }

        public async override Task<CancelOrderCommandResult> Handle(
            CancelOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();
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

            if (order.OrderDate < DateTime.Now.AddDays(-10))
            {
                throw new DomainException("", "Đã quá hạn hủy có thể đơn hàng");
            }

            var voucher = await this.voucherRepository.GetVoucher(order.VoucherId);
            if (voucher is null)
            {
                throw new DomainException("", "Voucher không tồn tại");
            }

            await this.voucherRepository.UpdateVoucherQuantity(order.VoucherId, voucher.Quantity + 1);

            switch ((OrderStatus)order.Status)
            {
                case OrderStatus.Pending:
                case OrderStatus.NeedToConfirm:
                case OrderStatus.Preparing:
                    break;
                case OrderStatus.Deliver:
                    throw new DomainException(
                        HttpStatusCode.BadRequest.GetHashCode().ToString(),
                        nameof(HttpStatusCode.BadRequest)
                    );
                case OrderStatus.Received:
                    throw new DomainException(
                        HttpStatusCode.BadRequest.GetHashCode().ToString(),
                        nameof(HttpStatusCode.BadRequest)
                    );
                case OrderStatus.Cancel:
                    throw new DomainException(
                        HttpStatusCode.BadRequest.GetHashCode().ToString(),
                        nameof(HttpStatusCode.BadRequest)
                    );
                case OrderStatus.Refund:
                    throw new DomainException(
                        HttpStatusCode.BadRequest.GetHashCode().ToString(),
                        nameof(HttpStatusCode.BadRequest)
                    );
                default:
                    throw new DomainException(
                        HttpStatusCode.BadRequest.GetHashCode().ToString(),
                        nameof(HttpStatusCode.BadRequest)
                    );
            }

            await this.orderRepository.CancelOrder(order.Id, customerId.Value);

            var result = new CancelOrderCommandResult(true, "");

            if (order.IsOrdered == OrderType.Ordered.GetHashCode())
            {
                if (order.IsPaid == PayType.Paid.GetHashCode())
                {
                    await this.RefundProductBothQuantity(request.OrderId);
                    result.Message = "Hãy liên hệ shop để được hoàn tiền";
                }
                else
                {
                    await this.RefundProductQuantity(request.OrderId);
                }
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
