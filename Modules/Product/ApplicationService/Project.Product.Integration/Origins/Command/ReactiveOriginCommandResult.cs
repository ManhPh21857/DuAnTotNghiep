

namespace Project.Product.Integration.Origins.Command
{
    public class ReactiveOriginCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveOriginCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
