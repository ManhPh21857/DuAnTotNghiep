namespace Project.Sales.Integration.Vouchers.Command
{
    public class UpdateVoucherCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateVoucherCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
