using Project.Core.ApplicationService.Queries;
using Project.Core.Domain.Constants;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Query;

namespace Project.Sales.ApplicationService.Orders.Query
{
    public class GetShopOrderQueryHandler : QueryHandler<GetShopOrderQuery, GetShopOrderQueryResult>
    {
        private readonly IOrderRepository orderRepository;

        public GetShopOrderQueryHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async override Task<GetShopOrderQueryResult> Handle(
            GetShopOrderQuery request,
            CancellationToken cancellationToken
        )
        {
            int skip = (request.Page - 1) * CommonConst.ORDER_PAGE_SIZE;
            int take = CommonConst.ORDER_PAGE_SIZE;

            var result = await this.orderRepository.GetShopOrder(skip, take, request.Filter);

            int totalPage = result.Item2 / CommonConst.EMPLOYEE_PAGE_SIZE;
            if (result.Item2 % CommonConst.EMPLOYEE_PAGE_SIZE > 0)
            {
                totalPage++;
            }

            return new GetShopOrderQueryResult(result.Item1, totalPage);
        }
    }
}
