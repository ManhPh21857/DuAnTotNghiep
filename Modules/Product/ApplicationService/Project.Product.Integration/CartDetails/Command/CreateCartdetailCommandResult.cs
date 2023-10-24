
namespace Project.Product.Integration.CartDetails.Command
{
    public class CreateCartdetailCommandResult
    {
        public bool Result { get; set; }
        public CreateCartdetailCommandResult(bool result)
        {
            Result = result;
        }
    }
}
