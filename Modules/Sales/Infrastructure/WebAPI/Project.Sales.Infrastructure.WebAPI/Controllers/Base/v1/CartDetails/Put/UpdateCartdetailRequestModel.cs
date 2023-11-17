
using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Put
{
    public class UpdateCartdetailRequestModel
    {
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }

    }
    
}
