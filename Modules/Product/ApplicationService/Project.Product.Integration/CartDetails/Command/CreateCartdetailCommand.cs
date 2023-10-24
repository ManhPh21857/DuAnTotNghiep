
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.CartDetails.Command
{
    public class CreateCartdetailCommand : ICommand<CreateCartdetailCommandResult>
    {
        public int Cart_id { get; set; }
        public int Product_detail_id { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public CreateCartdetailCommand(int cart_id, int product_detail_id, float price, int quantity)
        {
            this.Cart_id = cart_id;
            this.Product_detail_id = product_detail_id;
            this.Price = price;
            this.Quantity = quantity;
        }
    }
}
