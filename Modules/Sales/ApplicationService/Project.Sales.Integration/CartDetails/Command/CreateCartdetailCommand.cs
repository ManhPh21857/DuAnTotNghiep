using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.CartDetails.Command
{
    public class CreateCartdetailCommand : ICommand<CreateCartdetailCommandResult>
    {
        public int UserId { get; set; }
        public int CartId { get; set; }
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }

        public CreateCartdetailCommand(int userId,int cartId ,int productDetailId, int quantity)
        {
            UserId = userId;
            CartId = cartId;
            ProductDetailId = productDetailId;
            Quantity = quantity;
        }
    }
}
