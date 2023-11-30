using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.SaleCounters.Command
{
    public class CreateOrderDetailCommand : ICommand<CreateOrderDetailCommandResult>
    {
        //Order
        
        //OrderDetail
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int VoucherId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        public CreateOrderDetailCommand( int productId, int colorId, int sizeId,int orderId, 
            int productDetailId, int voucherId, float price, int quantity)
        {

            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;
            OrderId = orderId;
            ProductDetailId = productDetailId;
            VoucherId = voucherId;
            Price = price;
            Quantity = quantity;
        }
    }
}
