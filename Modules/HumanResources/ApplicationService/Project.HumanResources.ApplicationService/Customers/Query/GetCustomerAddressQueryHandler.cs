using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Integration.Customers.Query;

namespace Project.HumanResources.ApplicationService.Customers.Query
{
    public class GetCustomerAddressQueryHandler : QueryHandler<GetCustomerAddressQuery, GetCustomerAddressQueryResult>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;

        public GetCustomerAddressQueryHandler(ICustomerRepository customerRepository, ISessionInfo sessionInfo)
        {
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<GetCustomerAddressQueryResult> Handle(
            GetCustomerAddressQuery request,
            CancellationToken cancellationToken
        )
        {
            int userId = this.sessionInfo.UserId.value;

            int? customerId = await this.customerRepository.GetCustomerId(userId);
            if (customerId is null)
            {
                throw new DomainException("", "");
            }

            var customerAddress = await this.customerRepository.GetCustomerAddress(customerId.Value);

            return new GetCustomerAddressQueryResult(customerAddress);
        }
    }
}
