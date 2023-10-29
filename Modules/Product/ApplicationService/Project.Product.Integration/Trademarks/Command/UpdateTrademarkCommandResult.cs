
namespace Project.Product.Integration.Trademarks.Command
{
    public class UpdateTrademarkCommandResult
    {
        public bool IsSuccess { get; set; }
        public UpdateTrademarkCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
