using Project.Sales.Domain.Customers;

namespace Project.Sales.Integration.Customers.Query
{
    public class GetCustomerQueryResult
    {
        public IEnumerable<CustomerInfo> Customer { get; set; }

        public GetCustomerQueryResult(IEnumerable<CustomerInfo> customer)
        {
            this.Customer = customer;
        }


    }
}
