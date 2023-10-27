using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Get
{
    public class SupplierModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
