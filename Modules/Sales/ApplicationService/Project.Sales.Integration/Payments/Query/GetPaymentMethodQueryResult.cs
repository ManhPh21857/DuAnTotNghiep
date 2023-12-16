using Project.Sales.Domain.Payments;

namespace Project.Sales.Integration.Payments.Query
{
    public class GetPaymentMethodQueryResult
    {
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }

        public GetPaymentMethodQueryResult(IEnumerable<PaymentMethod> paymentMethods)
        {
            this.PaymentMethods = paymentMethods;
        }
    }
}
