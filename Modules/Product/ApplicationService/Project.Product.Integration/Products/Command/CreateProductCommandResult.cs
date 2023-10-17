namespace Project.Product.Integration.Products.Command
{
    public class CreateProductCommandResult
    {
        public bool IsSuccess { get; set; }

        public CreateProductCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}