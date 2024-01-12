namespace Project.Product.Integration.Materials.Command
{
    public class ReactiveMaterialCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveMaterialCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
