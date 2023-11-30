
using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Integration.CartDetails.Command;

namespace Project.Sales.Integration.CartDetails.Command
{
    public class UpdateCartdetailCommand : ICommand<UpdateCartdetailCommandResult>
    {
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public UpdateCartdetailCommand(int productDetailId, int quantity, byte[]? dataVersion, int productId, int colorId, int sizeId)
        {
            ProductDetailId = productDetailId;
            Quantity = quantity;
            DataVersion = dataVersion;
            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;
        }
    }
}
