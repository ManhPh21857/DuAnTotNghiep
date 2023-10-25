
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.CartDetails.Command
{
    public class DeleteCartdetailCommand : ICommand<DeleteCartdetailCommandResult>
    {
        public int Cartid { get; set; }
        public int Productdetailid { get; set; }
        public DeleteCartdetailCommand(int cartid, int productdetailid)
        {
            this.Cartid = cartid;
            this.Productdetailid = productdetailid;
        }
    }
}
