

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Get
{
    public class TrademarkModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
