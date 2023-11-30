namespace Project.Sales.Integration.SaleCounters.Command
{
    public class CreateOrderDetailCommandResult
    {
        public bool IsSuccess { get; set; }
        public CreateOrderDetailCommandResult(bool issuccess)
        {
            IsSuccess = issuccess;
        }
    }
}
