namespace Project.Product.Integration.Colors.Command
{
    public class UpdateColorCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateColorCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
