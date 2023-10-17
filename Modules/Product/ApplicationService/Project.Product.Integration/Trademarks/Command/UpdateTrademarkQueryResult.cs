using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class UpdateTrademarkQueryResult
    {
        public bool Result { get; set; }
        public UpdateTrademarkQueryResult(bool result)
        {
            Result = result;
        }
    }
}
