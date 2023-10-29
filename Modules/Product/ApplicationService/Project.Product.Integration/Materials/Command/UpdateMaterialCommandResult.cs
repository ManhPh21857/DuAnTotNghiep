
namespace Project.Product.Integration.Materials.Command
{
    public class UpdateMaterialCommandResult
    {
        public bool IsSuccess { get; set; }
        public UpdateMaterialCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

    }
}
