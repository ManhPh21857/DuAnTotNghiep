
namespace Project.Product.Integration.Trademarks.Command
{
    public class UpdateTrademarkCommandResult
    {
        public bool Result { get; set; }
        public UpdateTrademarkCommandResult(bool result)
        {
            Result = result;
        }
    }
}
