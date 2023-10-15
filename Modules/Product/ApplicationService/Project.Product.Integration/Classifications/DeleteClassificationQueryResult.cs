using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications
{
    public class DeleteClassificationQueryResult
    {
        public bool Result { get; set; }

        public DeleteClassificationQueryResult(bool result)
        {
            Result = result;
        }
    }
}
