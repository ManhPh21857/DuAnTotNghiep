using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Domain.Vouchers;
using Project.Sales.Integration.SaleCounters.Command;

namespace Project.Sales.ApplicationService.SaleCounters.Command
{
    public class CreateOrderDetailCommandHandler : CommandHandler<CreateOrderDetailCommand, CreateOrderDetailCommandResult>
    {
        private readonly ISaleCounterRepository saleCounterRepository;
        private readonly ISessionInfo sessionInfo;
        private readonly IVoucherRepository voucherRepository;
        public CreateOrderDetailCommandHandler(ISaleCounterRepository saleCounterRepository, IVoucherRepository voucherRepository, ISessionInfo sessionInfo)
        {
            this.saleCounterRepository = saleCounterRepository;
            this.sessionInfo = sessionInfo;
            this.voucherRepository = voucherRepository;
        }

        public async override Task<CreateOrderDetailCommandResult> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var code = Guid.NewGuid();

            
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
            
            var order = new OrderInfo
            {
                EmployeeId = this.sessionInfo.UserId.value,
                OrderCode = code,
                Address = "Tại Quầy",
                FullName = request.Order.FullName,
                CustomerId = request.Order.CustomerId,
                MerchandiseSubtotal = request.Order.MerchandiseSubtotal,
                OrderTotal = request.Order.OrderTotal,
                PhoneNumber = request.Order.PhoneNumber,
                VoucherApplied = request.Order.VoucherApplied,
                VoucherId = request.Order.VoucherId,
                PaymentMethodId = 2,
                IsOrder = 1,
                IsPaid = 1,
                OrderDate = DateTime.Now,
                PaymentDate = DateTime.Now,
                Status = 3
            };
            var id = await this.saleCounterRepository.CreateOrder(order);
            foreach (var item in request.Orderdetails)
            {
                var a = await this.saleCounterRepository.GetQuantity(item.ProductId, item.ColorId, item.SizeId);
                await this.saleCounterRepository.CreateOrderDetail(new OrderDetailInfo
                {
                    OrderId = id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ColorId = item.ColorId,
                    SizeId = item.SizeId,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    TotalQuantity = a-item.Quantity
                });
                UpdateQuantityInfo update = new UpdateQuantityInfo()
                {
                    ProductId = item.ProductId,
                    ColorId = item.ColorId,
                    SizeId = item.SizeId,
                    Quantity = a - item.Quantity
                };

                await this.saleCounterRepository.UpdateQuantity(update);
            }
            return new CreateOrderDetailCommandResult(true);
        }
    }
}
