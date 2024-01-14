namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class NewCustomerResponseModel
    {
        public IEnumerable<NewCustomerModel> NewCustomers { get; set; }

        public NewCustomerResponseModel(IEnumerable<NewCustomerModel> newCustomers)
        {
            NewCustomers = newCustomers;
        }
    }
}
