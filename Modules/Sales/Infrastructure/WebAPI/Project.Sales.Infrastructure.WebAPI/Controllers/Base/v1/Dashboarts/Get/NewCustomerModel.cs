namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class NewCustomerModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
