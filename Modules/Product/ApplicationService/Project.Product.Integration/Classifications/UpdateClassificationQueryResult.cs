using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications
{
    public class UpdateClassificationQueryResult
    {
        public bool Result { get; set; }

        public UpdateClassificationQueryResult(bool result)
        {
            Result = result;
        }
    }
}
