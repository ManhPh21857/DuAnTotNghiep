using Project.HumanResources.Domain.Customers;

namespace Project.HumanResources.Integration.Customers.Query
{
    public class GetCustomerQueryResult
    {
        public CustomerInfo Customer { get; set; }

        public GetCustomerQueryResult(CustomerInfo customer)
        {
            this.Customer = customer;
        }
    }
}
