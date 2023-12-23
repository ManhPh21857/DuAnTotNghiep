namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Customers.Get
{
    public class CustomerResponseModel
    {
        public IEnumerable<CustomerModel> Customer { get; set; }

        public CustomerResponseModel(IEnumerable<CustomerModel> customer)
        {
            this.Customer = customer;
        }
    }
}
