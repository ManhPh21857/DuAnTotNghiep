﻿using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
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
            createOder.EmployeeId = sessionInfo.UserId.Value;
            var ordercode = new Guid();
            createOder.OrderCode = ordercode;
            createOder.Address = "Tại Quầy";
            createOder.MerchandiseSubtotal = request.Order.MerchandiseSubtotal;
            createOder.PaymentMethodId = 2;
            createOder.IsOrder = 1;
            createOder.IsPaid = 1;
            createOder.OrderDate = DateTime.Now;
            createOder.PaymentDate = DateTime.Now;
            createOder.Status = 3;
            var id = await this.saleCounterRepository.CreateOrder(createOder);
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
                    TotalQuantity = a - item.Quantity
                });
                UpdateQuantityInfo he = new UpdateQuantityInfo()
                {
                    ProductId = item.ProductId,
                    ColorId = item.ColorId,
                    SizeId = item.SizeId,
                    Quantity = a - item.Quantity
                };

                await this.saleCounterRepository.UpdateQuantity(he);
            }
            return new CreateOrderDetailCommandResult(true);
        }
    }
}
