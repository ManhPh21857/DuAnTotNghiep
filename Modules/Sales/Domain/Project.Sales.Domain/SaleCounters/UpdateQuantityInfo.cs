namespace Project.Sales.Domain.SaleCounters
{
    public class UpdateQuantityInfo
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public int Actual_Quantity { get; set; }
    }
}
