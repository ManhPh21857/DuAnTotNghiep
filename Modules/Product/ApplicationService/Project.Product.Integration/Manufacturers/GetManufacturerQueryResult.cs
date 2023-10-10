using Project.Product.Domain.Manufacturers1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Manufacturers
{
    public class GetManufacturerQueryResult
    {
        public IList<ManufacturerInfo> Manufacturers { get; set; }
        public GetManufacturerQueryResult(IList<ManufacturerInfo> manufacturers)
        {
            Manufacturers = manufacturers;
        }
       
    }
}
