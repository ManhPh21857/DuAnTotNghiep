namespace Project.Sales.Domain.Dashboards
{
    public interface IDashboardsRepository
    {
        Task<IEnumerable<DashboardInfo>> GetCustomerTotal();
        Task<IEnumerable<ProductTotalInfo>> GetProductTotal();
        Task<IEnumerable<OrderTotalInfo>> GetOrderTotal();
        Task<IEnumerable<SoldOutProductInfo>> SoldOutProducts();
        Task<IEnumerable<OrderUnconfimred>> GetOrderUnconfimred();
        Task<IEnumerable<NewCustomerInfo>> GetNewCustomer();
        Task<IEnumerable<SoldOutProductDetailInfo>> GetSoldOutProductDetail();
    }
}
