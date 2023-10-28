
namespace Project.Product.Integration.Trademarks.Command
{
    public class DeleteTrademarkCommandResult
    {
        public bool IsSuccess { get; set; }
        public DeleteTrademarkCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
