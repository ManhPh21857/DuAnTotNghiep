using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Integration.Employees.Query;

namespace Project.HumanResources.ApplicationService.Employees.Query
{
    public class EmployeeQueryHandler : QueryHandler<EmployeeQuery, EmployeeQueryResult>
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async override Task<EmployeeQueryResult> Handle(
            EmployeeQuery request,
            CancellationToken cancellationToken
        )
        {
            int skip = (request.PageNumber - 1) * 10;
            int take = 10;

            var result = await this.employeeRepository.GetEmployees(skip, take);

            return new EmployeeQueryResult(result);
        }
    }
}
