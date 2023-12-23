namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Get
{
    public class GetCustomerResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
