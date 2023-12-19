namespace Project.Sales.Domain.Orders
{
    public class FinishOrderOnlinePaymentParam
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public int IsOrdered { get; set; }
        public int IsPaid { get; set; }
        public int Status { get; set; }
    }
}
