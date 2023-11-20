namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get
{
    public class SaleCounterInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
