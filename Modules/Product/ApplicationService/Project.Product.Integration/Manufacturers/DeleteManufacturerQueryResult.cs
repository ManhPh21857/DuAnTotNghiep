using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Manufacturers
{
    public class DeleteManufacturerQueryResult
    {
        public bool Result { get; set; }

        public DeleteManufacturerQueryResult(bool result)
        {
            Result = result;
        }
    }
}
