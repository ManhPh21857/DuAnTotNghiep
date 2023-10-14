using Project.Product.Domain.Trademarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Query
{
    public class GetTrademarkQueryResult
    {
        public IList<TrademarkInfo> Trademark { get; set; }
        public GetTrademarkQueryResult(IList<TrademarkInfo> trademark)
        {
            Trademark = trademark;
}
        }
    }
