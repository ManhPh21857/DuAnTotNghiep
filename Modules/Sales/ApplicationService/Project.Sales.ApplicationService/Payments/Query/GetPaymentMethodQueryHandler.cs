using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Payments;
using Project.Sales.Integration.Payments.Query;

namespace Project.Sales.ApplicationService.Payments.Query
{
    public class GetPaymentMethodQueryHandler : QueryHandler<GetPaymentMethodQuery, GetPaymentMethodQueryResult>
    {
        private readonly IPaymentRepository paymentRepository;

        public GetPaymentMethodQueryHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async override Task<GetPaymentMethodQueryResult> Handle(
            GetPaymentMethodQuery request,
            CancellationToken cancellationToken
        )
        {
            var paymentMethods = await this.paymentRepository.GetPaymentMethods();

            return new GetPaymentMethodQueryResult(paymentMethods);
        }
    }
}
