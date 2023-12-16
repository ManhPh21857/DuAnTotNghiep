namespace Project.HumanResources.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task InsertCustomer(InsertCustomerParam param);
        Task<int?> GetCustomerId(int userId);
        Task<IEnumerable<CustomerAddress>> GetCustomerAddress(int customerId);
        Task<CustomerInfo> GetCustomerInfo(int userId);
    }
}
