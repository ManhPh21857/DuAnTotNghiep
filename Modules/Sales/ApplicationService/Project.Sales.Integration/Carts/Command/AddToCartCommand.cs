using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Carts.Command
{
    public class AddToCartCommand : ICommand<AddToCartCommandResult>
    {
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }

        public AddToCartCommand(int productDetailId, int quantity)
        {
            this.ProductDetailId = productDetailId;
            this.Quantity = quantity;
        }
    }
}
