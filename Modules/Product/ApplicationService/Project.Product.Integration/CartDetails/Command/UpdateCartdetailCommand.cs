
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.CartDetails.Command
{
    public class UpdateCartdetailCommand : ICommand<UpdateCartdetailCommandResult>
    {
        public int Cart_id { get; set; }
        public int Product_detail_id { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public UpdateCartdetailCommand(int cartid, int productdetailid, float price, int quantity)
        {
            this.Cart_id = cartid;
            this.Product_detail_id = productdetailid;
            this.Price = price;
            this.Quantity = quantity;
        }
    }
}
