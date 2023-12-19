namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Payments.Get
{
    public class GetPaymentMethodResponseModel
    {
        public IEnumerable<PaymentMethodModel> PaymentMethods { get; set; }

        public GetPaymentMethodResponseModel()
        {
            this.PaymentMethods = new List<PaymentMethodModel>();
        }
    }
}
