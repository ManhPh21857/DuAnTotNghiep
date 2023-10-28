﻿
namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get
{
    public class CartdetailModel
    {
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Image { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
