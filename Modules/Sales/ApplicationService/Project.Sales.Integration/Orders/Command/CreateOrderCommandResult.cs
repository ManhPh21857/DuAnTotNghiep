namespace Project.Sales.Integration.Orders.Command
{
    public class CreateOrderCommandResult
    {
        public bool IsSuccess { get; set; }
        public string? PayUrl { get; set; }

        public CreateOrderCommandResult(bool isSuccess, string? payUrl)
        {
            this.IsSuccess = isSuccess;
            this.PayUrl = payUrl;
        }
    }
}
