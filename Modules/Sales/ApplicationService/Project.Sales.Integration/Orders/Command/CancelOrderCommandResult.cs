namespace Project.Sales.Integration.Orders.Command
{
    public class CancelOrderCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public CancelOrderCommandResult(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
    }
}
