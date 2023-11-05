
namespace Project.Product.Integration.CartDetails.Command
{
    public class DeleteCartdetailCommandResult
    {
        public bool IsSuccess { get; set; }
        public DeleteCartdetailCommandResult(bool result)
        {
            IsSuccess = result;
        }
    }
}
