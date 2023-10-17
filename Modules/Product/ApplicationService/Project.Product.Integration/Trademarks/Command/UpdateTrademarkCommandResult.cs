using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class UpdateTrademarkCommandResult
    {
        public bool Result { get; set; }
        public UpdateTrademarkCommandResult(bool result)
        {
            Result = result;
        }
    }
}
