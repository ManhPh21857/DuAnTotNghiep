using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.Core.Domain.Constants;
using Project.HumanResources.Domain.Employees;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Query;

namespace Project.Sales.ApplicationService.Orders.Query
{
    public class GetShopOrderQueryHandler : QueryHandler<GetShopOrderQuery, GetShopOrderQueryResult>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ISessionInfo sessionInfo;
        private readonly IEmployeeRepository employeeRepository;

        public GetShopOrderQueryHandler(
            IOrderRepository orderRepository,
            ISessionInfo sessionInfo,
            IEmployeeRepository employeeRepository
        )
        {
            this.orderRepository = orderRepository;
            this.sessionInfo = sessionInfo;
            this.employeeRepository = employeeRepository;
        }

        public async override Task<GetShopOrderQueryResult> Handle(
            GetShopOrderQuery request,
            CancellationToken cancellationToken
        )
        {
            int skip = (request.Page - 1) * CommonConst.ORDER_PAGE_SIZE;
            int take = CommonConst.ORDER_PAGE_SIZE;

            int? employeeId = null;
            if (request.Model == 1)
            {
                int userId = this.sessionInfo.UserId.Value;
                employeeId = await this.employeeRepository.GetEmployeeId(userId);
                if (employeeId is null)
                {
                    throw new DomainException("", "không tìm thấy nhân viên");
                }
            }

            var result = await this.orderRepository.GetShopOrder(skip, take, employeeId);

            int totalPage = result.Item2 / CommonConst.EMPLOYEE_PAGE_SIZE;
            if (result.Item2 % CommonConst.EMPLOYEE_PAGE_SIZE > 0)
            {
                totalPage++;
            }

            return new GetShopOrderQueryResult(result.Item1, totalPage);
        }
    }
}
