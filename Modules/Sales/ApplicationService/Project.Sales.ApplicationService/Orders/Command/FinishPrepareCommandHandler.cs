using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Domain.Users;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Orders;
using Project.Sales.Integration.Orders.Command;

namespace Project.Sales.ApplicationService.Orders.Command
{
    public class FinishPrepareCommandHandler : CommandHandler<FinishPrepareCommand, FinishPrepareCommandResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ISessionInfo sessionInfo;

        public FinishPrepareCommandHandler(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IUserRepository userRepository,
            ISessionInfo sessionInfo,
            IEmployeeRepository employeeRepository
        )
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
            this.sessionInfo = sessionInfo;
            this.employeeRepository = employeeRepository;
        }

        public async override Task<FinishPrepareCommandResult> Handle(
            FinishPrepareCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();
            int userId = this.sessionInfo.UserId.Value;

            int? employeeId = await this.employeeRepository.GetEmployeeId(userId);

            if (employeeId is null)
            {
                throw new DomainException("", "Nhân viên không tồn tại");
            }

            int? assignees = await this.orderRepository.GetOrderAssign(request.Id);

            if (assignees == employeeId)
            {
                await this.orderRepository.UpdateOrderStatus(
                    request.Id,
                    OrderStatus.Deliver.GetHashCode(),
                    request.DataVersion
                );
            }
            else
            {
                var userRoles = await this.userRepository.GetUserRoles(userId);
                if (userRoles.Any(x => x.Id == Role.OrderManager.GetHashCode()))
                {
                    await this.orderRepository.AssignEmployee(request.Id, employeeId.Value, request.DataVersion);

                    var temp = await this.orderRepository.GetOrder(request.Id);

                    await this.orderRepository.UpdateOrderStatus(
                        request.Id,
                        OrderStatus.Deliver.GetHashCode(),
                        temp.DataVersion
                    );
                }
                else
                {
                    throw new DomainException("", "Bạn không có quyền xử lý đơn hàng này");
                }
            }

            var orderDetails = await this.orderRepository.GetOrderDetails(request.Id);
            foreach (var item in orderDetails)
            {
                var productDetail = await this.productRepository.GetProductDetails(item.ProductId, item.ColorId, item.SizeId);

                await this.productRepository.UpdateProductDetailActualQuantity(
                    productDetail.Id,
                    productDetail.ActualQuantity - item.Quantity
                );
            }

            scope.Complete();
            return new FinishPrepareCommandResult(true);
        }
    }
}
