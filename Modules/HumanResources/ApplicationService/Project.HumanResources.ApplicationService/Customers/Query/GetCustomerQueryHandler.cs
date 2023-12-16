using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Integration.Customers.Query;

namespace Project.HumanResources.ApplicationService.Customers.Query
{
    public class GetCustomerQueryHandler : QueryHandler<GetCustomerQuery, GetCustomerQueryResult>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository, ISessionInfo sessionInfo)
        {
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<GetCustomerQueryResult> Handle(
            GetCustomerQuery request,
            CancellationToken cancellationToken
        )
        {
            var userId = this.sessionInfo.UserId.value;

            var customer = await this.customerRepository.GetCustomerInfo(userId);

            return new GetCustomerQueryResult(customer);
        }
    }
}
