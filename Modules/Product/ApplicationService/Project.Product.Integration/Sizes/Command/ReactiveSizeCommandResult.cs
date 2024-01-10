namespace Project.Product.Integration.Sizes.Command
{
    public class ReactiveSizeCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveSizeCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
