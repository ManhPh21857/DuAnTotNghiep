namespace Project.Sales.Integration.Payments.Command
{
    public class CreateMoMoPaymentCommandResult
    {
        public string PayUrl { get; set; }

        public CreateMoMoPaymentCommandResult(string payUrl)
        {
            this.PayUrl = payUrl;
        }
    }
}
