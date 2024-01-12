using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Orders.Command
{
    public class CancelOrderCommand : ICommand<CancelOrderCommandResult>
    {
        public int OrderId { get; set; }
        public bool IsGarbage { get; set; }

        public CancelOrderCommand(int orderId, bool isGarbage)
        {
            this.OrderId = orderId;
            this.IsGarbage = isGarbage;
        }
    }
}
