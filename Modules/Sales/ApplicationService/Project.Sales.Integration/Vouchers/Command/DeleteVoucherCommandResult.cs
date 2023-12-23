namespace Project.Sales.Integration.Vouchers.Command
{
    public class DeleteVoucherCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteVoucherCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
