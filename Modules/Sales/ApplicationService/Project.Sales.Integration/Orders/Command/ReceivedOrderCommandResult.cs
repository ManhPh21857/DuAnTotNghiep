namespace Project.Sales.Integration.Orders.Command
{
    public class ReceivedOrderCommandResult
    {
        public bool IsSuccess { get; set; }

        public ReceivedOrderCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
