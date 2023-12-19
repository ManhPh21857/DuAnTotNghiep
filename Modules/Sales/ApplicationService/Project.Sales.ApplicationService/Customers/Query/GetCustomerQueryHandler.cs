using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Customers;
using Project.Sales.Integration.Customers.Query;

namespace Project.Sales.ApplicationService.Customers.Query
{
    public class GetCustomerQueryHandler : QueryHandler<GetCustomerQuery, GetCustomerQueryResult>
    {
        private readonly ICustomerRepository custom;

        public GetCustomerQueryHandler(ICustomerRepository custom)
        {
            this.custom = custom;
        }
        public async override Task<GetCustomerQueryResult> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var Customers = await this.custom.GetCustomer();
            return new GetCustomerQueryResult(Customers);
        }
    }
}
