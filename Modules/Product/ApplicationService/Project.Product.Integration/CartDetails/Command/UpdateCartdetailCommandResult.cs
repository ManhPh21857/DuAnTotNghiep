
namespace Project.Product.Integration.CartDetails.Command
{
    public class UpdateCartdetailCommandResult
    {
        public bool Result { get; set; }
        public UpdateCartdetailCommandResult(bool result)
        {
            Result = result;
        }
    }
}
