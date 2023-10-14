using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class CreateTrademarkQueryResult
    {
        public bool Result { get; set; }
        public CreateTrademarkQueryResult(bool result)
        {
            Result = result;
        }
    }
}
