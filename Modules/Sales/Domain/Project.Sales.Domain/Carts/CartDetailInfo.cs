﻿namespace Project.Sales.Domain.Carts
{
    public class CartDetailInfo
    {
        public int? CartId { get; set; }
        public int? Id { get; set; }
        public int ProductDetailId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string? Color { get; set; }
        public int SizeId { get; set; }
        public string? Size { get; set; }
        public string? Image { get; set; }
        public string? ProductName { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
