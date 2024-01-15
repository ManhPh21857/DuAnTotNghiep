using MediatR;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Integration.Services;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Orders;
using Project.Sales.Domain.Vouchers;
using Project.Sales.Integration.Orders.Command;
using System.Net;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class CancelOrderCommandHandler : CommandHandler<CancelOrderCommand, CancelOrderCommandResult>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository productRepository;
        private readonly IVoucherRepository voucherRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ISender mediator;

        public CancelOrderCommandHandler(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IVoucherRepository voucherRepository,
            IOrderRepository orderRepository,
            ISender mediator
        )
        {
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.voucherRepository = voucherRepository;
            this.orderRepository = orderRepository;
            this.mediator = mediator;
        }

        public async override Task<CancelOrderCommandResult> Handle(
            CancelOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            var order = (await this.orderRepository.GetOrders(null, request.OrderId)).FirstOrDefault();

            if (order is null)
            {
                throw new DomainException("", "Đơn hàng không tồn tại");
            }

            if (order.OrderDate < DateTime.Now.AddDays(-10))
            {
                throw new DomainException("", "Đã quá hạn hủy đơn hàng");
            }

            if (order.VoucherId.HasValue)
            {
                var voucher = await this.voucherRepository.GetVoucher(order.VoucherId.Value);
                if (voucher is not null)
                {
                    await this.voucherRepository.UpdateVoucherQuantity(order.VoucherId.Value, voucher.Quantity + 1);
                }
            }

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

            await this.orderRepository.CancelOrder(order.Id);

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

            if (request.IsForced && order.CustomerId.HasValue)
            {
                await this.SendMail(order.CustomerId.Value);
            }

            scope.Complete();
            return result;
        }

        private async Task SendMail(int customerId)
        {
            var emailTo = await this.customerRepository.GetCustomerEmail(customerId);
            if (!string.IsNullOrEmpty(emailTo))
            {
                var input = new SendMailRequest(new[] { emailTo },
                    "Thông báo hủy đơn hàng",
                    "Đơn hàng của ông/bà đã bị hủy");
                await this.mediator.Send(input);
            }
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
