namespace Project.HumanResources.Integration.Customers.Command
{
    public class UpdateAddressDefaultCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateAddressDefaultCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
