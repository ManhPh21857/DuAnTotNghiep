using Project.Product.Domain.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Manufacturers
{
    public class AddManufacturerQueryResult 
    {
        public bool Result { get; set; }

        public AddManufacturerQueryResult(bool result)
        {
            Result = result;
        }


    }
}
