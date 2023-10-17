using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Materials
{
    public class CreateMaterialsQueryResult
    {
        public bool Result { get; set; }
        public CreateMaterialsQueryResult(bool result)
        {
            Result = result;
        }
    }
}
