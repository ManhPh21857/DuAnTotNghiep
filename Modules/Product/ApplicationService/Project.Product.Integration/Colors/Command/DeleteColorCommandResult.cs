namespace Project.Product.Integration.Colors.Command
{
    public class DeleteColorCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteColorCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
