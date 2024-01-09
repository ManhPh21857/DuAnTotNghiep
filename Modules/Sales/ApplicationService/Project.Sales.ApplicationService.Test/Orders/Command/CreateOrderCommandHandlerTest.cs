using MediatR;
using Project.Core.Domain;
using Project.HumanResources.Domain.Customers;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Carts;
using Project.Sales.Domain.Orders;
using Project.Sales.Domain.Vouchers;
using Moq;

namespace Project.Sales.ApplicationService.Test.Orders.Command
{
    public class CreateOrderCommandHandlerTest
    {
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<IProductRepository> mockProductRepository;
        private readonly Mock<IVoucherRepository> mockVoucherRepository;
        private readonly Mock<IOrderRepository> mockOrderRepository;
        private readonly Mock<ICartRepository> mockCartRepository;
        private readonly Mock<ISessionInfo> mockSessionInfo;
        private readonly Mock<ISender> mockMediator;

        public CreateOrderCommandHandlerTest(
        )
        {
            this.mockCustomerRepository = new Mock<ICustomerRepository>();
            this.mockProductRepository = new Mock<IProductRepository>();
            this.mockVoucherRepository = new Mock<IVoucherRepository>();
            this.mockOrderRepository = new Mock<IOrderRepository>();
            this.mockCartRepository = new Mock<ICartRepository>();
            this.mockSessionInfo = new Mock<ISessionInfo>();
            this.mockMediator = new Mock<ISender>();
        }
    }
}
