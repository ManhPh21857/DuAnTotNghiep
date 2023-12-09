using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Carts.Command
{
    public class UpdateCartDetailCommand : ICommand<UpdateCartDetailCommandResult>
    {
        public int ProductDetailId { get; set; }
        public int ProductDetailIdNew { get; set; }
        public int Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
