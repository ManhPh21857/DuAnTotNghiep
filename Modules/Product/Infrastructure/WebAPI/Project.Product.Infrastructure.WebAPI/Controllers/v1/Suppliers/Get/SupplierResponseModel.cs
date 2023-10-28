using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Get
{
    public class SupplierResponseModel
    {
        public IEnumerable<SupplierModel> Suppliers { get; set; }

        public SupplierResponseModel()
        {
            Suppliers = new List<SupplierModel>();
        }
    }
}
