namespace Project.Sales.Integration.Payments.Command
{
    public class FinishOrderCommandResult
    {
        public bool IsSuccess { get; set; }

        public FinishOrderCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
