using Project.Product.Domain.Trademarks;
namespace Project.Product.Integration.Trademarks.Query
{
    public class GetTrademarkQueryResult
    {
        public IList<TrademarkInfo> Trademarks { get; set; }
        public GetTrademarkQueryResult(IList<TrademarkInfo> trademarks)
        {
            Trademarks = trademarks;
}
        }
    }
