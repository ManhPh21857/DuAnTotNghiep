using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Orders.Command
{
    public class CancelOrderCommand : ICommand<CancelOrderCommandResult>
    {
        public int OrderId { get; set; }

        public CancelOrderCommand(int orderId)
        {
            this.OrderId = orderId;
        }
    }
}
