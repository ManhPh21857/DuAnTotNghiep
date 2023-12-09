namespace Project.Sales.Integration.Carts.Command
{
    public class DeleteCartDetailCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteCartDetailCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
