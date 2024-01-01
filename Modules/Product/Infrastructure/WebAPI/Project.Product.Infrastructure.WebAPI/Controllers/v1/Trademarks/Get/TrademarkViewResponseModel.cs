namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Get
{
    public class TrademarkViewResponseModel
    {
        public IEnumerable<TrademarkViewModel> Trademarks { get; set; }

        public TrademarkViewResponseModel()
        {
            this.Trademarks = new List<TrademarkViewModel>();
        }
    }
}
