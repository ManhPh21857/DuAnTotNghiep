﻿namespace Project.Sales.Domain.SaleCounters
{
    public class OrderDetailInfo
    {
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int VoucherId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
