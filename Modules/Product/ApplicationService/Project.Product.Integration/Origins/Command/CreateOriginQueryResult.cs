using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class CreateOriginQueryResult 
    {
        public bool Result { get; set; }
        public CreateOriginQueryResult(bool result)
        {
            Result = result;
        }
    }
}
