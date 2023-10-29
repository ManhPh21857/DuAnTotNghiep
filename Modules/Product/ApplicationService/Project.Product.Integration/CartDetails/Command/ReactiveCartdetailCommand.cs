
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.CartDetails.Command
{
    public class ReactiveCartdetailCommand : ICommand<ReactiveCartdetailCommandResult>
    {
        public int CartId { get; set; }
        public int ProductDetailId { get; set; }
        public byte[]? DataVersion { get; set; }
        public ReactiveCartdetailCommand(int cartid, int productdetailid, byte[]? dataversion)
        {
            this.CartId = cartid;
            this.ProductDetailId = productdetailid;
            this.DataVersion = dataversion;
        }
    }
}
