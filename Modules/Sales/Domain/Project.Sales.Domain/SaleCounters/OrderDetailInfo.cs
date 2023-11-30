namespace Project.Sales.Domain.SaleCounters
{
    public class OrderDetailInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int AddressId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int ProductDetailId { get; set; }
        public int VoucherId { get; set; }
        public float Total { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
