namespace Project.Product.Integration.Sizes.Command
{
    public class UpdateSizeCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateSizeCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
