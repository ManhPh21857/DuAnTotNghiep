namespace Project.Sales.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerInfo>> GetCustomer();
    }
}
