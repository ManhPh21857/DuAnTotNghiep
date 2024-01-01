using Project.Product.Domain.Suppliers;

namespace Project.Product.Integration.Suppliers.Query
{
    public class GetSupplierQueryResult
    {
        public IEnumerable<SupplierInfo> Suppliers { get; set; }

        public GetSupplierQueryResult(IEnumerable<SupplierInfo> supplier)
        {
            this.Suppliers = supplier;
        }
    }
}
