namespace Project.Product.Integration.Origins.Command
{
    public class DeleteOriginCommandResult
    {
        public bool IsSuccess { get; set; }
        public DeleteOriginCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
