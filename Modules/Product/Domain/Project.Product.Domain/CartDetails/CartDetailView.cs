﻿namespace Project.Product.Domain.CartDetails
{
    public class CartDetailView
    {
        public int? ProductDetailId { get; set; }
        public int? CartId { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        public float? Price { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
