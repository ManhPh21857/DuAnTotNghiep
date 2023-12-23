using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Integration.Employees.Query;

namespace Project.HumanResources.ApplicationService.Employees.Query
{
    public class GetEmployeeOrderQueryHandler : QueryHandler<GetEmployeeOrderQuery, GetEmployeeOrderQueryResult>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetEmployeeOrderQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async override Task<GetEmployeeOrderQueryResult> Handle(
            GetEmployeeOrderQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await this.employeeRepository.GetEmployeeOrder();

            return new GetEmployeeOrderQueryResult(result);
        }
    }
}
