namespace Project.Sales.Domain.Carts
{
    public class CreateCartDetailParam
    {
        public int CartId { get; set; }
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
    }
}
