﻿namespace Project.Sales.Domain.Orders
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public Guid OrderCode { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public float MerchandiseSubtotal { get; set; }
        public float ShippingFee { get; set; }
        public float ShippingDiscountSubtotal { get; set; }
        public int? VoucherId { get; set; }
        public float VoucherApplied { get; set; }
        public float OrderTotal { get; set; }
        public int PaymentMethodId { get; set; }
        public int IsOrdered { get; set; }
        public int IsPaid { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public byte[]? DataVersion { get; set; }
        public IEnumerable<OrderDetailInfo> OrderDetails { get; set; }

        public OrderInfo()
        {
            this.OrderDetails = new List<OrderDetailInfo>();
        }
    }
}
