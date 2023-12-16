using MediatR;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Customers;
using Project.Sales.Domain.Carts;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Command;
using Project.Sales.Integration.Payments.Command;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class CreateOrderCommandHandler : CommandHandler<CreateOrderCommand, CreateOrderCommandResult>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ICartRepository cartRepository;
        private readonly ISessionInfo sessionInfo;
        private readonly ISender mediator;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            ISessionInfo sessionInfo,
            ICartRepository cartRepository,
            ISender mediator
        )
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
            this.cartRepository = cartRepository;
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
                VoucherApplied = request.Order.VoucherApplied,
                OrderTotal = request.Order.OrderTotal,
                PaymentMethodId = request.Order.PaymentMethodId
            };

            if (request.Order.PaymentMethodId == PaymentMethod.MoMoPayment.GetHashCode())
            {
                createOrderParam.EmployeeId = 0;
                createOrderParam.IsOrder = OrderType.NotYet.GetHashCode();
                createOrderParam.IsPaid = PayType.NotYet.GetHashCode();
                createOrderParam.OrderDate = null;
                createOrderParam.PaymentDate = null;
                createOrderParam.Status = OrderStatus.NeedToConfirm.GetHashCode();
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
                await this.orderRepository.CreateOrderDetail(new CreateOrderDetailParam
                    {
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        ColorId = item.ColorId,
                        SizeId = item.SizeId,
                        Price = item.Price,
                        Quantity = item.Quantity
                    }
                );

                await this.cartRepository.DeleteCartDetail(new DeleteCartDetailParam
                    {
                        CartId = item.CartId,
                        ProductDetailId = item.ProductDetailId,
                        DataVersion = item.DataVersion
                    }
                );
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
