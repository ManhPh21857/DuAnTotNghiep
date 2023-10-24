
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.CartDetails.Command
{
    public class DeleteCartdetailCommand : ICommand<DeleteCartdetailCommandResult>
    {
        public int Cart_id { get; set; }
        public int Product_detail_id { get; set; }
        public DeleteCartdetailCommand(int cartid, int productdetailid)
        {
            this.Cart_id = cartid;
            this.Product_detail_id = productdetailid;
        }
    }
}
