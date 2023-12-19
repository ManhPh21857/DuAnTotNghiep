using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Payments.Command
{
    public class CreateMoMoPaymentCommand : ICommand<CreateMoMoPaymentCommandResult>
    {
        public string CustomerName { get; set; }
        public int OrderId { get; set; }
        public Guid OrderCode { get; set; }
        public float Amount { get; set; }

        public CreateMoMoPaymentCommand(string customerName, int orderId, Guid orderCode, float amount)
        {
            this.CustomerName = customerName;
            this.OrderId = orderId;
            this.OrderCode = orderCode;
            this.Amount = amount;
        }
    }
}
