
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.CartDetails.Command
{
    public class CreateCartdetailCommand : ICommand<CreateCartdetailCommandResult>
    {
        public int Cartid { get; set; }
        public int Productdetailid { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public CreateCartdetailCommand(int cartid, int productdetailid, float price, int quantity)
        {
            this.Cartid = cartid;
            this.Productdetailid = productdetailid;
            this.Price = price;
            this.Quantity = quantity;
        }
    }
}
