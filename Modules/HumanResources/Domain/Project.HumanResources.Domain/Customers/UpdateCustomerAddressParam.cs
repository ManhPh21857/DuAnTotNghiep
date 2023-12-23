namespace Project.HumanResources.Domain.Customers
{
    public class UpdateCustomerAddressParam
    {
        public int? Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string AddressDetail { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
