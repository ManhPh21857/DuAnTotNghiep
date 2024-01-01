using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Employees.Query;

namespace Project.HumanResources.ApplicationService.Employees.Query
{
    public class GetEmployeeInfoQueryHandler : QueryHandler<GetEmployeeInfoQuery, GetEmployeeInfoQueryResult>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ISessionInfo sessionInfo;
        private readonly IUserRepository userRepository;

        public GetEmployeeInfoQueryHandler(
            IEmployeeRepository employeeRepository,
            ISessionInfo sessionInfo,
            IUserRepository userRepository
        )
        {
            this.employeeRepository = employeeRepository;
            this.sessionInfo = sessionInfo;
            this.userRepository = userRepository;
        }

        public async override Task<GetEmployeeInfoQueryResult> Handle(
            GetEmployeeInfoQuery request,
            CancellationToken cancellationToken
        )
        {
            int userId = this.sessionInfo.UserId.Value;

            var employee = await this.employeeRepository.GetEmployeeInfo(userId);

            var userRole = await this.userRepository.GetUserRoles(userId);

            return new GetEmployeeInfoQueryResult(employee, userRole);
        }
    }
}
