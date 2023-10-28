
namespace Project.Product.Integration.Classifications.Command
{
    public class UpdateClassificationCommandResult
    {
        public bool IsSuccess { get; set; }
        public UpdateClassificationCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

    }
}
