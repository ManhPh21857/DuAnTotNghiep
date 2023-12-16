using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Payments.Command
{
    public class FinishOrderCommand : ICommand<FinishOrderCommandResult>
    {
        public int Id { get; set; }
        public string AccessKey { get; set; }
        public string RequestId { get; set; }
        public string OrderId { get; set; }

        public FinishOrderCommand(int id, string accessKey, string requestId, string orderId)
        {
            this.Id = id;
            this.AccessKey = accessKey;
            this.RequestId = requestId;
            this.OrderId = orderId;
        }
    }
}
