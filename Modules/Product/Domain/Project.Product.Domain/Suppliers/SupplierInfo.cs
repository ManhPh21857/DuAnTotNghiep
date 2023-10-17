using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Suppliers
{
    public class SupplierInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressID { get; set; }
        public int Status { get; set; }
    }
}
