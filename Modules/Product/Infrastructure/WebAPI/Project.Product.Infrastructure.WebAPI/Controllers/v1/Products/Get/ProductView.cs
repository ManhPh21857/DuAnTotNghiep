﻿namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ClassificationName { get; set; }
        public string MaterialName { get; set; }
        public string SupplierName { get; set; }
        public string TrademarkName { get; set; }
        public string OriginName { get; set; }
    }
}