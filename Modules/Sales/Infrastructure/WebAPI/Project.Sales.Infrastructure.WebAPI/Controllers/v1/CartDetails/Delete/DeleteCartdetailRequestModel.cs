
namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Delete
{
    public class DeleteCartdetailRequestModel
    {
        public int CartId { get; set; }
        public int ProductDetailId { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
