using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class UpdateOriginCommandResult
    {
        public bool Result { get; set; }
        public UpdateOriginCommandResult(bool result)
        {
            Result = result;
        }
    }
}
