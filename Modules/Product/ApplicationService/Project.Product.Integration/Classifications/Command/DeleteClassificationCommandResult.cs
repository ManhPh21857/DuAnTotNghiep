
namespace Project.Product.Integration.Classifications.Command
{
    public class DeleteClassificationCommandResult
    {
        public bool IsSuccess { get; set; }
        public DeleteClassificationCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
