using Moq;
using Project.Sales.ApplicationService.Orders.Command;
using Project.Sales.Integration.Orders.Command;
using Xunit;

namespace Project.Sales.ApplicationService.Test.Orders.Command
{
    public class AssignOrderCommandHandlerTest
    {
        private readonly MockOrderRepository mockOrderRepository;

        public AssignOrderCommandHandlerTest()
        {
            this.mockOrderRepository = new MockOrderRepository();
        }

        [Fact]
        public async Task AssignEmployee()
        {
            //arrange
            var handler = new AssignOrderCommandHandler(this.mockOrderRepository.Object);
            var command = new AssignOrderCommand(1, 1, new byte[] { 0x10 });

            //actual
            var result = await handler.Handle(command, It.IsAny<CancellationToken>());

            //assert
            Assert.True(result.IsSuccess);
        }
    }
}
