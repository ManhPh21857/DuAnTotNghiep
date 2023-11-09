
namespace Project.Sales.Integration.CartDetails.Command
{
    public class UpdateCartdetailCommandResult
    {
        public bool IsSuccess { get; set; }
        public UpdateCartdetailCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
