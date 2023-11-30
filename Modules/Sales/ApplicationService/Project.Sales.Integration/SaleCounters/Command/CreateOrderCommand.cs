using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.SaleCounters.Command
{
    public class CreateOrderCommand :ICommand<CreateOrderCommandResult>
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int AddressId { get; set; }
        public float Total { get; set; }
        public CreateOrderCommand(int userId, int employeeId, int addressId, float total)
        {
            UserId = userId;
            EmployeeId = employeeId;
            AddressId = addressId;
            Total = total;
        }
    }
}
