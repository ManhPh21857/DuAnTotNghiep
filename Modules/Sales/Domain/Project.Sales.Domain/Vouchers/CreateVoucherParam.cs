﻿namespace Project.Sales.Domain.Vouchers
{
    public class CreateVoucherParam
    {
        public string Name { get; set; }
        public int VoucherType { get; set; }
        public float MinimumPrice { get; set; }
        public float Discount { get; set; }
        public int DiscountType { get; set; }
        public float MaximumDiscount { get; set; }
        public DateTime ApplyPeriodStart { get; set; }
        public DateTime ApplyPeriodEnd { get; set; }
        public int Quantity { get; set; }
        public int CreatedBy { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}
