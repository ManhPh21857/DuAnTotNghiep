namespace Project.Sales.Domain.Orders
{
    public class GetOrderFilter
    {
        public string? Name { get; set; }
        public List<int>? ListStatus { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
