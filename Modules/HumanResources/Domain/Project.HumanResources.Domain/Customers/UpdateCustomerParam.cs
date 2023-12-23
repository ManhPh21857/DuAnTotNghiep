namespace Project.HumanResources.Domain.Customers
{
    public class UpdateCustomerParam
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
        public int Sex { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
