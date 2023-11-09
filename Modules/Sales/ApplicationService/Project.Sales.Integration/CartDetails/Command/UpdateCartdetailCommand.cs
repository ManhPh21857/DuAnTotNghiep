
using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Integration.CartDetails.Command;

namespace Project.Sales.Integration.CartDetails.Command
{
    public class UpdateCartdetailCommand : ICommand<UpdateCartdetailCommandResult>
    {
        public IEnumerable<CartDetailInfo> Cartdetails { get; set; }

        public UpdateCartdetailCommand(IEnumerable<CartDetailInfo> cartdetails)
        {
            Cartdetails = cartdetails;
        }
    }
}
