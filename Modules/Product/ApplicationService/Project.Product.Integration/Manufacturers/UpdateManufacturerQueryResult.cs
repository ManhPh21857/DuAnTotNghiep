using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Manufacturers
{
    public class UpdateManufacturerQueryResult
    {
        public bool Result { get; set; }

        public UpdateManufacturerQueryResult(bool result)
        {
            Result = result;
        }
    }
}
