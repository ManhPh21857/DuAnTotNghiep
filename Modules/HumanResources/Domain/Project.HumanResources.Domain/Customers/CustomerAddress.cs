namespace Project.HumanResources.Domain.Customers
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string AddressDetail { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
