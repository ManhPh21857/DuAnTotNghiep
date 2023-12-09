namespace Project.Sales.Domain.SaleCounters
{
    public class OrderInfo
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int AddressId { get; set; }
        public float Total { get; set; }
    }
}
