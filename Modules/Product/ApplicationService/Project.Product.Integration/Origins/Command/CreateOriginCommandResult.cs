using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class CreateOriginCommandResult 
    {
        public bool Result { get; set; }
        public CreateOriginCommandResult(bool result)
        {
            Result = result;
        }
    }
}
