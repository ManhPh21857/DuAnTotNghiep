namespace Project.HumanResources.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task InsertCustomer(InsertCustomerParam param);
        Task<int?> GetCustomerId(int userId);
        Task<IEnumerable<CustomerAddress>> GetCustomerAddress(int customerId);
        Task InsertCustomerAddress(UpdateCustomerAddressParam param);
        Task UpdateCustomerAddress(UpdateCustomerAddressParam param);
        Task UpdateDefaultAddress(int customerId, int id);
        Task<CustomerInfo> GetCustomerInfo(int userId);
        Task UpdateCustomer(UpdateCustomerParam param);
    }
}
