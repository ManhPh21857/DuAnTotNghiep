using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Materials
{
    public class UpdateMaterialsQueryResult
    {
        public bool Result { get; set; }
        public UpdateMaterialsQueryResult(bool result)
        {
            Result = result;
        }
    }
}
