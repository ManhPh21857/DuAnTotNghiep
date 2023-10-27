using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Suppliers
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<SupplierInfo>> GetSupplier(int? id);
        Task AddSupplier(SupplierInfo Supplier);
        Task UpdateSupplier(SupplierInfo Supplier);
        Task DeleteSupplier(SupplierInfo Supplier);
        Task ReactiveSupplier(SupplierInfo Supplier);
        Task<SupplierInfo> CheckSupplierName(string name,string address);
    }
}
