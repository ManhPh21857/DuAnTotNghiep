namespace Project.Sales.Domain.SaleCounters
{
    public class OrderInfo
    {
        public Guid OrderCode { get; set; }
        public int CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public float MerchandiseSubtotal { get; set; }
        public float ShippingFee { get; set; }
        public float ShippingDiscountSubtotal { get; set; }
        public float VoucherApplied { get; set; }
        public float OrderTotal { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? IsOrder { get; set; }
        public int? IsPaid { get; set; }
        public int? Status { get; set; }

        //order_code, customer_id, employee_id, full_name, phone_number, address, merchandise_subtotal, shipping_fee, voucher_applied, order_total, payment_method_id, order_date, payment_date, is_ordered, is_paid, status
    }
}
