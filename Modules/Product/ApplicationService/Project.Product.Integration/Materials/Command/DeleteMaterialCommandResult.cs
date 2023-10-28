
namespace Project.Product.Integration.Materials.Command
{
    public class DeleteMaterialCommandResult
    {
        public bool IsSuccess { get; set; }
        public DeleteMaterialCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
