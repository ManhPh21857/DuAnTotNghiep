using Moq;
using Project.Core.Domain;
using Project.Core.Domain.User;
using Project.HumanResources.Domain.Customers;
using Project.Product.Domain.Products;
using Project.Sales.ApplicationService.Orders.Command;
using Project.Sales.Domain.Orders;
using Project.Sales.Domain.Vouchers;
using Project.Sales.Integration.Orders.Command;
using Xunit;

namespace Project.Sales.ApplicationService.Test.Orders.Command
{
    public class CancelOrderCommandHandlerTest
    {
        private readonly Mock<IProductRepository> mockProductRepository;
        private readonly Mock<IOrderRepository> mockOrderRepository;
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<ISessionInfo> mockSessionInfo;
        private readonly Mock<IVoucherRepository> mockVoucherRepository;

        public CancelOrderCommandHandlerTest()
        {
            this.mockProductRepository = new Mock<IProductRepository>();
            this.mockOrderRepository = new MockOrderRepository();
            this.mockCustomerRepository = new Mock<ICustomerRepository>();
            this.mockSessionInfo = new Mock<ISessionInfo>();
            this.mockVoucherRepository = new Mock<IVoucherRepository>();

            this.mockSessionInfo.SetupGet(x => x.UserId).Returns(new UserId(1));
            this.mockCustomerRepository
                .Setup(x => x.GetCustomerId(It.IsAny<int>()))
                .ReturnsAsync(1);

            this.mockOrderRepository
                .Setup(x => x.GetOrders(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<OrderInfo>
                    {
                        new()
                        {
                            Id = 1,
                            OrderCode = new Guid(),
                            CustomerId = 1,
                            CustomerName = "1",
                            FullName = "1",
                            PhoneNumber = "1",
                            Address = "1",
                            MerchandiseSubtotal = 1,
                            ShippingFee = 1,
                            ShippingDiscountSubtotal = 1,
                            VoucherId = 0,
                            VoucherApplied = 1,
                            OrderTotal = 1,
                            PaymentMethodId = 1,
                            IsOrdered = 1,
                            IsPaid = 1,
                            OrderDate = new DateTime(2024, 1, 1),
                            Status = 1,
                            EmployeeId = 1,
                            EmployeeName = "1",
                            DataVersion = new byte[] { 0x10 },
                            OrderDetails = new List<OrderDetailInfo>
                            {
                                new()
                                {
                                    OrderId = 1,
                                    ProductId = 1,
                                    ProductName = "1",
                                    Image = "a",
                                    ColorId = 1,
                                    Color = "1",
                                    SizeId = 1,
                                    Size = "1",
                                    Price = 1,
                                    Quantity = 1
                                }
                            }
                        }
                    }
                );

            this.mockVoucherRepository
                .Setup(x => x.GetVoucher(It.IsAny<int>()))
                .ReturnsAsync(new Voucher
                    {
                        Id = 1,
                        Name = "1",
                        VoucherType = 1,
                        MinimumPrice = 1,
                        Discount = 1,
                        DiscountType = 1,
                        MaximumDiscount = 1,
                        ApplyPeriodStart = new DateTime(2024, 1, 1),
                        ApplyPeriodEnd = new DateTime(2024, 1, 10),
                        Quantity = 1,
                        CreatedBy = "1",
                        LastUpdateBy = "1",
                        IsDeleted = 1,
                        DataVersion = new byte[] { 0x10 }
                    }
                );

            this.mockOrderRepository
                .Setup(x => x.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new List<OrderDetailInfo>
                    {
                        new()
                        {
                            OrderId = 1,
                            ProductId = 1,
                            ProductName = "1",
                            Image = "1",
                            ColorId = 1,
                            Color = "1",
                            SizeId = 0,
                            Size = "1",
                            Price = 1,
                            Quantity = 1
                        }
                    }
                );

            this.mockProductRepository
                .Setup(x => x.GetProductDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new ProductDetailInfo
                    {
                        Id = 1,
                        ProductId = 1,
                        ColorId = 1,
                        SizeId = 1,
                        ImportPrice = 1,
                        Price = 1,
                        Quantity = 1,
                        ActualQuantity = 1,
                        ImportQuantity = 1,
                        DataVersion = new byte[] { 0x10 }
                    }
                );

        }

        private CancelOrderCommandHandler Handler()
        {
            return new CancelOrderCommandHandler(
                this.mockProductRepository.Object,
                this.mockOrderRepository.Object,
                this.mockCustomerRepository.Object,
                this.mockSessionInfo.Object,
                this.mockVoucherRepository.Object
            );
        }

        [Fact]
        public async Task CancelOrder_Success()
        {
            var handler = this.Handler();

            var command = this.Command();

            var result = await handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task CancelOrder_CustomerNotFound()
        {
            int? response = null;

            this.mockCustomerRepository
                .Setup(x => x.GetCustomerId(It.IsAny<int>()))
                .ReturnsAsync(response);

            var handler = this.Handler();

            var command = this.Command();

            var result = await Assert.ThrowsAsync<DomainException>(()
                => handler.Handle(command, It.IsAny<CancellationToken>()));

            Assert.Equal("Khách hàng không tồn tại", result.ErrorMessage);
        }

        [Fact]
        public async Task CancelOrder_OrderNotFound()
        {
            this.mockOrderRepository
                .Setup(x => x.GetOrders(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<OrderInfo>());

            var handler = this.Handler();

            var command = this.Command();

            var result = await Assert.ThrowsAsync<DomainException>(()
                => handler.Handle(command, It.IsAny<CancellationToken>()));

            Assert.Equal("Đơn hàng không tồn tại", result.ErrorMessage);
        }

        [Fact]
        public async Task CancelOrder_ExpireTime()
        {
            this.mockOrderRepository
                .Setup(x => x.GetOrders(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<OrderInfo>
                    {
                        new()
                        {
                            Id = 1,
                            OrderCode = new Guid(),
                            CustomerId = 1,
                            CustomerName = "1",
                            FullName = "1",
                            PhoneNumber = "1",
                            Address = "1",
                            MerchandiseSubtotal = 1,
                            ShippingFee = 1,
                            ShippingDiscountSubtotal = 1,
                            VoucherId = 0,
                            VoucherApplied = 1,
                            OrderTotal = 1,
                            PaymentMethodId = 1,
                            IsOrdered = 1,
                            IsPaid = 1,
                            OrderDate = DateTime.Now.AddDays(-15),
                            Status = 1,
                            EmployeeId = 1,
                            EmployeeName = "1",
                            DataVersion = new byte[] { 0x10 },
                            OrderDetails = new List<OrderDetailInfo>
                            {
                                new()
                                {
                                    OrderId = 1,
                                    ProductId = 1,
                                    ProductName = "1",
                                    Image = "a",
                                    ColorId = 1,
                                    Color = "1",
                                    SizeId = 1,
                                    Size = "1",
                                    Price = 1,
                                    Quantity = 1
                                }
                            }
                        }
                    }
                );

            var handler = this.Handler();

            var command = this.Command();

            var result = await Assert.ThrowsAsync<DomainException>(()
                => handler.Handle(command, It.IsAny<CancellationToken>()));

            Assert.Equal("Đã quá hạn hủy có thể đơn hàng", result.ErrorMessage);
        }

        private CancelOrderCommand Command()
        {
            return new CancelOrderCommand(1, false);
        }
    }
}
