namespace Project.Product.Integration.Origins.Command
{
    public class UpdateOriginCommandResult
    {
        public bool IsSuccess { get; set; }
        public UpdateOriginCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
