
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Delete
{
    public class DeleteCartdetailModel
    {
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
