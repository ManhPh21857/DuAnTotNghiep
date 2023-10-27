
namespace Project.Product.Integration.Trademarks.Command
{
    public class ReactiveTrademarkCommandResult
    {
        public bool Result { get; set; }
        public ReactiveTrademarkCommandResult(bool result)
        {
            Result =result;
        }
    }
}
