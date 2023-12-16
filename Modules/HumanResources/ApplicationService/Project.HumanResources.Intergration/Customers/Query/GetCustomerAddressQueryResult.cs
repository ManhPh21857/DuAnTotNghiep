using Project.HumanResources.Domain.Customers;

namespace Project.HumanResources.Integration.Customers.Query
{
    public class GetCustomerAddressQueryResult
    {
        public IEnumerable<CustomerAddress> CustomerAddresses { get; set; }

        public GetCustomerAddressQueryResult(IEnumerable<CustomerAddress> customerAddresses)
        {
            CustomerAddresses = customerAddresses;
        }
    }
}
