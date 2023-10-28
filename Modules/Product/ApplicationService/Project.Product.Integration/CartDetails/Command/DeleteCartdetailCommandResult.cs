
namespace Project.Product.Integration.CartDetails.Command
{
    public class DeleteCartdetailCommandResult
    {
        public bool Result { get; set; }
        public DeleteCartdetailCommandResult(bool result)
        {
            Result = result;
        }
    }
}
