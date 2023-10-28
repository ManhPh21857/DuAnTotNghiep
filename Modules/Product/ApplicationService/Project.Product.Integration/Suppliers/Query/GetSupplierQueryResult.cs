
using Project.Product.Domain.Suppliers;

namespace Project.Product.Integration.Suppliers.Query
{
    public class GetSupplierQueryResult
    {
        public IList<SupplierInfo> Supplier { get; set; }
        public GetSupplierQueryResult(IList<SupplierInfo> supplier)
        {
            Supplier = supplier;
        }
       
    }
}
