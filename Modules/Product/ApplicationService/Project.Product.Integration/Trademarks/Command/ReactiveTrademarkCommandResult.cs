
namespace Project.Product.Integration.Trademarks.Command
{
    public class ReactiveTrademarkCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveTrademarkCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
