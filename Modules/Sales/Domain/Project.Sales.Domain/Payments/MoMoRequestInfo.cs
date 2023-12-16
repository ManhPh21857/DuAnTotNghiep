namespace Project.Sales.Domain.Payments
{
    public class MoMoRequestInfo
    {
        public string FullName { get; set; }
        public string OrderId { get; set; }
        public string OrderCode  { get; set; }
        public string OrderInfo { get; set; }
        public double Amount { get; set; }
        public string ExtraData { get; set; }
    }
}
