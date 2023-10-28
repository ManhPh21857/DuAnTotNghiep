namespace Project.Product.Integration.Products.Command
{
    public class DeleteProductCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteProductCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
