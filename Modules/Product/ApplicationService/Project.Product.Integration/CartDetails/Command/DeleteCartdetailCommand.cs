
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.CartDetails.Command
{
    public class DeleteCartdetailCommand : ICommand<DeleteCartdetailCommandResult>
    {
        public int CartId { get; set; }
        public int ProductDetailId { get; set; }
        public byte[]? DataVersion { get; set; }
        public DeleteCartdetailCommand(int cartid, int productdetailid, byte[]? dataversion)
        {
            this.CartId = cartid;
            this.ProductDetailId = productdetailid;
            this.DataVersion = dataversion;
        }
    }
}
