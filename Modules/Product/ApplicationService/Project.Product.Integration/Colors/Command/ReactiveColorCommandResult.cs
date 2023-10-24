namespace Project.Product.Integration.Colors.Command
{
    public class ReactiveColorCommandResult
    {
        public bool IsSuccess { get; set; }

        public ReactiveColorCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
