namespace Project.Sales.Domain.Payments
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
