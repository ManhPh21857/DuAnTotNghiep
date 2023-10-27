
namespace Project.Product.Integration.Trademarks.Command
{
    public class DeleteTrademarkCommandResult
    {
        public bool Result { get; set; }
        public DeleteTrademarkCommandResult(bool result)
        {
            Result = result;
        }
    }
}
