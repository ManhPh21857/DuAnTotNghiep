﻿namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post
{
    public class ProductDetailModel
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public float ImportPrice { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}