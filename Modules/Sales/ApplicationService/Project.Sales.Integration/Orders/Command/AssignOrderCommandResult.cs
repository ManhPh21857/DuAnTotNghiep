namespace Project.Sales.Integration.Orders.Command
{
    public class AssignOrderCommandResult
    {
        public bool IsSuccess { get; set; }

        public AssignOrderCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
