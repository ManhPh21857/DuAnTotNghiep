
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.CartDetails;

namespace Project.Product.Integration.CartDetails.Command
{
    public class UpdateCartdetailCommand : ICommand<UpdateCartdetailCommandResult>
    {
        public IEnumerable<CartdetailInfo> Cartdetails { get; set; }

        public UpdateCartdetailCommand(IEnumerable<CartdetailInfo> cartdetails)
        {
            Cartdetails = cartdetails;
        }
    }
}
