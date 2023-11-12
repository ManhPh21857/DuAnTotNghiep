
using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Put
{
    public class UpdateCartdetailModel
    {
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public byte[]? DataVersion { get; set; }


    }
   
}
