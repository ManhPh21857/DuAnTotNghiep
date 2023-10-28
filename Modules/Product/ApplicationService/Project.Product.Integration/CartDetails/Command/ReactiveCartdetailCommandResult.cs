
namespace Project.Product.Integration.CartDetails.Command
{
    public class ReactiveCartdetailCommandResult
    {
        public bool Result { get; set; }
        public ReactiveCartdetailCommandResult(bool result)
        {
            Result = result;
        }
    }
}
