using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Orders.Command
{
    public class ReceivedOrderCommand : ICommand<ReceivedOrderCommandResult>
    {
        public int Id { get; set; }

        public ReceivedOrderCommand(int id)
        {
            this.Id = id;
        }
    }
}
