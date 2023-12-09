namespace Project.Sales.Domain.Carts
{
    public class DeleteCartDetailParam
    {
        public int CartId { get; set; }
        public int ProductDetailId { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
