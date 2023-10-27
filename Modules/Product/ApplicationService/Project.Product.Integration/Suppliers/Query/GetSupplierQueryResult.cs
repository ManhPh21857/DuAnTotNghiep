
using Project.Product.Domain.Suppliers;

namespace Project.Product.Integration.Suppliers.Query
{
    public class GetSupplierQueryResult
    {
        public IList<SupplierInfo> Suppliers { get; set; }
        public GetSupplierQueryResult(IList<SupplierInfo> supplier)
        {
            Suppliers = supplier;
        }
       
    }
}
