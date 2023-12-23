namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Customers.Get
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public int? Is_Deleted { get; set; }
    }
}
