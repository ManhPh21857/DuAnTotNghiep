namespace Project.Product.Integration.Sizes.Command
{
    public class DeleteSizeCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteSizeCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
