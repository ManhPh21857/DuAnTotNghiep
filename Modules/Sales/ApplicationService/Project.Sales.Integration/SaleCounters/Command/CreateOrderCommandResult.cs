namespace Project.Sales.Integration.SaleCounters.Command
{
    public class CreateOrderCommandResult
    {
        public bool IsSuccess { get; set; }
        public CreateOrderCommandResult(bool issuccess)
        {
            IsSuccess = issuccess;
        }
    }
}
