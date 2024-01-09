using MediatR;
using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Customers;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Carts;
using Project.Sales.Domain.Orders;
using Project.Sales.Domain.Vouchers;
using Project.Sales.Integration.Orders.Command;
using Project.Sales.Integration.Payments.Command;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class CreateOrderCommandHandler : CommandHandler<CreateOrderCommand, CreateOrderCommandResult>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository productRepository;
        private readonly IVoucherRepository voucherRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;
        private readonly ISessionInfo sessionInfo;
        private readonly ISender mediator;

        public CreateOrderCommandHandler(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IVoucherRepository voucherRepository,
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            ISessionInfo sessionInfo,
            ISender mediator
        )
        {
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.voucherRepository = voucherRepository;
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.sessionInfo = sessionInfo;
            this.mediator = mediator;
        }

        public async override Task<CreateOrderCommandResult> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            int userId = this.sessionInfo.UserId.value;
            int? customerId = await this.customerRepository.GetCustomerId(userId);

            if (customerId is null)
            {
                throw new DomainException("", "Tài khoản không tồn tại hoặc không có quyền thực hiện giao dịch");
            }

            var orderCode = Guid.NewGuid();

            var voucher = await this.voucherRepository.GetVoucher(request.Order.VoucherId);
            if (voucher is null)
            {
                throw new DomainException("", "Voucher không tồn tại");
            }

            if (voucher.Quantity < 1)
            {
                throw new DomainException("", "Voucher đã hết số lần sử dụng");
            }

            await this.voucherRepository.UpdateVoucherQuantity(request.Order.VoucherId, voucher.Quantity - 1);

            var createOrderParam = new CreateOrderParam
            {
                OrderCode = orderCode,
                CustomerId = customerId.Value,
                FullName = request.Order.FullName,
                PhoneNumber = request.Order.PhoneNumber,
                Address = request.Order.Address,
                MerchandiseSubtotal = request.Order.MerchandiseSubtotal,
                ShippingFee = request.Order.ShippingFee,
                ShippingDiscountSubtotal = request.Order.ShippingDiscountSubtotal,
                VoucherId = request.Order.VoucherId,
                VoucherApplied = request.Order.VoucherApplied,
                OrderTotal = request.Order.OrderTotal,
                PaymentMethodId = request.Order.PaymentMethodId
            };

            if (request.Order.PaymentMethodId == PaymentMethod.MoMoPayment.GetHashCode())
            {
                createOrderParam.EmployeeId = 0;
                createOrderParam.IsOrder = OrderType.Ordered.GetHashCode();
                createOrderParam.IsPaid = PayType.NotYet.GetHashCode();
                createOrderParam.OrderDate = DateTime.Now;
                createOrderParam.PaymentDate = null;
                createOrderParam.Status = OrderStatus.Pending.GetHashCode();
            }
            else
            {
                createOrderParam.EmployeeId = null;
                createOrderParam.IsOrder = OrderType.Ordered.GetHashCode();
                createOrderParam.IsPaid = PayType.NotYet.GetHashCode();
                createOrderParam.OrderDate = DateTime.Now;
                createOrderParam.PaymentDate = null;
                createOrderParam.Status = OrderStatus.NeedToConfirm.GetHashCode();
            }

            //create order
            int orderId = await this.orderRepository.CreateOrder(createOrderParam);

            //create order detail
            foreach (var item in request.CartDetails)
            {
                var productDetail = await this.productRepository.GetProductDetailById(item.ProductDetailId) ??
                                    await this.productRepository.GetProductDetails(
                                        item.ProductId,
                                        item.ColorId,
                                        item.SizeId
                                    );
                if (productDetail is null)
                {
                    throw new DomainException("", "Sản phẩm không tồn tại");
                }

                if (productDetail.Quantity < item.Quantity)
                {
                    throw new DomainException("", "Số lượng không đủ");
                }

                await this.productRepository.UpdateProductDetailQuantity(
                    productDetail.Id,
                    productDetail.Quantity - item.Quantity
                );

                await this.orderRepository.CreateOrderDetail(new CreateOrderDetailParam
                    {
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName ?? "",
                        ColorId = item.ColorId,
                        SizeId = item.SizeId,
                        Price = item.Price,
                        Quantity = item.Quantity
                    }
                );

                if (!item.DataVersion.IsNullOrEmpty())
                {
                    await this.cartRepository.DeleteCartDetail(new DeleteCartDetailParam
                        {
                            CartId = item.CartId ?? 0,
                            ProductDetailId = item.ProductDetailId,
                            DataVersion = item.DataVersion
                        }
                    );
                }
            }

            var response = new CreateOrderCommandResult(true, null);

            if (request.Order.PaymentMethodId == PaymentMethod.MoMoPayment.GetHashCode())
            {
                var command = new CreateMoMoPaymentCommand(request.Order.FullName,
                    orderId,
                    orderCode,
                    request.Order.OrderTotal
                );

                var result = await this.mediator.Send(command, cancellationToken);
                response.PayUrl = result.PayUrl;
            }

            scope.Complete();

            return response;
        }
    }
}
