namespace Project.HumanResources.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task InsertCustomer(InsertCustomerParam param);
    }
}
