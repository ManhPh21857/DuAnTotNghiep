using Project.Core.ApplicationService.Queries;
using Project.Core.Domain.Constants;
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
            int skip = (request.PageNumber - 1) * CommonConst.EMPLOYEE_PAGE_SIZE;
            int take = CommonConst.EMPLOYEE_PAGE_SIZE;

            var result = await this.employeeRepository.GetEmployees(skip, take);
            int totalPage = result.TotalEmployee / CommonConst.EMPLOYEE_PAGE_SIZE;
            if (result.TotalEmployee % CommonConst.EMPLOYEE_PAGE_SIZE > 0)
            {
                totalPage++;
            }

            return new EmployeeQueryResult(result.Employees, totalPage);
        }
    }
}
