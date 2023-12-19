using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.Carts;
using Project.Sales.Domain.Orders;

namespace Project.Sales.Integration.Orders.Command
{
    public class CreateOrderCommand : ICommand<CreateOrderCommandResult>
    {
        public OrderInfo Order { get; set; }

        public IEnumerable<CartDetailInfo> CartDetails { get; set; }

        public CreateOrderCommand(OrderInfo order, IEnumerable<CartDetailInfo> cartDetails)
        {
            Order = order;
            CartDetails = cartDetails;
        }
    }
}
