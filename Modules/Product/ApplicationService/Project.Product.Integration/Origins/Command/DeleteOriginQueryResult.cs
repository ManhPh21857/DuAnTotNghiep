using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class DeleteOriginQueryResult
    {
        public bool Result { get; set; }
        public DeleteOriginQueryResult(bool result)
        {
            Result = result;
        }
    }
}
