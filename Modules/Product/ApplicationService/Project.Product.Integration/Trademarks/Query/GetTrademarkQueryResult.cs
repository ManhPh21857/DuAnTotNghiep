using Project.Product.Domain.Trademarks;

namespace Project.Product.Integration.Trademarks.Query
{
    public class GetTrademarkQueryResult
    {
        public IEnumerable<TrademarkInfo> Trademarks { get; set; }

        public GetTrademarkQueryResult(IEnumerable<TrademarkInfo> trademarks)
        {
            this.Trademarks = trademarks;
        }
    }
}
