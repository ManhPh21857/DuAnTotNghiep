using Project.Core.ApplicationService.Queries;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Employees.Query;

namespace Project.HumanResources.ApplicationService.Employees.Query
{
    public class GetEmployeeQueryHandler : QueryHandler<GetEmployeeQuery, GetEmployeeQueryResult>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUserRepository userRepository;

        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userRepository = userRepository;
        }

        public async override Task<GetEmployeeQueryResult> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employeeUser = await this.employeeRepository.GetEmployee(request.Id);

            if (employeeUser is not null)
            {
                employeeUser.Roles = await this.userRepository.GetUserRoles(employeeUser.UserId);
            }

            return new GetEmployeeQueryResult(employeeUser);
        }
    }
}
