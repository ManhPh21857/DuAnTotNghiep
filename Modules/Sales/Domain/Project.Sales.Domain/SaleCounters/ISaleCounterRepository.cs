namespace Project.Sales.Domain.SaleCounters
{
    public interface ISaleCounterRepository
    {
        Task<SaleCounterResponse> GetSaleCounterView(int skip, int take);
        Task<SaleCounterInfo> GetSaleCounterView(int id);
    }
}
