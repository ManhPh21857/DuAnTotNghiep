

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Get
{
    public class TrademarkResponseModel
    {
        public IEnumerable<TrademarkModel> Trademarks { get; set; }

        public TrademarkResponseModel()
        {
            Trademarks = new List<TrademarkModel>();
        }
    }
}
