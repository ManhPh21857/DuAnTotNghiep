using Project.Sales.Domain.Dashboards;

namespace Project.Sales.Integration.Dashboarts.Query
{
    public class GetNewCustomerQueryResult
    {
        public IEnumerable<NewCustomerInfo> NewCustomers { get; set; }

        public GetNewCustomerQueryResult(IEnumerable<NewCustomerInfo> newCustomers)
        {
            NewCustomers = newCustomers;
        }
    }
}
