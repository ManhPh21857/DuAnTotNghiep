using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class CreateTrademarkCommandResult
    {
        public bool Result { get; set; }
        public CreateTrademarkCommandResult(bool result)
        {
            Result = result;
        }
    }
}
