namespace Project.Product.Integration.Classifications.Command
{
    public class ReactiveClassificationCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveClassificationCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
