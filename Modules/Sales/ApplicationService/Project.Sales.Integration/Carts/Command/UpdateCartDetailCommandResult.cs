namespace Project.Sales.Integration.Carts.Command
{
    public class UpdateCartDetailCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateCartDetailCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
