using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Materials
{
    public class MaterialInfo
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
