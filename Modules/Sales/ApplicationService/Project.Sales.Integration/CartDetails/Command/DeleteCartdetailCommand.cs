using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.CartDetails.Command
{
    public class DeleteCartdetailCommand : ICommand<DeleteCartdetailCommandResult>
    {
        public int CartId { get; set; }
        public int ProductDetailId { get; set; }
        public byte[]? DataVersion { get; set; }

        public DeleteCartdetailCommand(int cartId, int productDetailId, byte[]? dataVersion)
        {
            CartId = cartId;
            ProductDetailId = productDetailId;
            DataVersion = dataVersion;
        }
    }
}
