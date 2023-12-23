namespace Project.HumanResources.Integration.Customers.Command
{
    public class UpdateCustomerAddressCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateCustomerAddressCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
