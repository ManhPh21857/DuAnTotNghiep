namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Payments.Post
{
    public class ContinuePaySessionRequestModel
    {
        public string CustomerName { get; set; }
        public int OrderId { get; set; }
        public Guid OrderCode { get; set; }
        public float Amount { get; set; }
    }
}
