using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.Carts;

namespace Project.Sales.Integration.Carts.Command
{
    public class DeleteCartDetailCommand : ICommand<DeleteCartDetailCommandResult>
    {
        public IEnumerable<DeleteCartDetailParam> CartDetails { get; set; }

        public DeleteCartDetailCommand(IEnumerable<DeleteCartDetailParam> cartDetails)
        {
            this.CartDetails = cartDetails;
        }
    }
}
