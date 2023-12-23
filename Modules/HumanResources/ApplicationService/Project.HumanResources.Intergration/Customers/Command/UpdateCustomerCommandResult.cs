namespace Project.HumanResources.Integration.Customers.Command
{
    public class UpdateCustomerCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateCustomerCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
