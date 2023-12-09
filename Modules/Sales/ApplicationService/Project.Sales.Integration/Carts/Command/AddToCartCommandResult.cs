namespace Project.Sales.Integration.Carts.Command
{
    public class AddToCartCommandResult
    {
        public bool IsSuccess { get; set; }

        public AddToCartCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
