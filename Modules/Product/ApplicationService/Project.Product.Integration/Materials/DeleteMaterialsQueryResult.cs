using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Materials
{
    public class DeleteMaterialsQueryResult
    {
        public bool Result { get; set; }
        public DeleteMaterialsQueryResult(bool result)
        {
            Result = result;
        }
    }
}
