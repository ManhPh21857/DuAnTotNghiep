namespace Project.Sales.Integration.Orders.Command
{
    public class FinishPrepareCommandResult
    {
        public bool IsSuccess { get; set; }

        public FinishPrepareCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
