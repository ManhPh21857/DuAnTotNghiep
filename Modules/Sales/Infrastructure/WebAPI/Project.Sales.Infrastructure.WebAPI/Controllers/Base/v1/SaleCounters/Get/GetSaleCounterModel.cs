namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get
{
    public class GetSaleCounterModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public byte[]? DataVersion { get; set; }
        public int Quantity { get; set; }
        public float AvgPrice { get; set; }
        public string ClassificationName { get; set; }
        public string MaterialName { get; set; }
    }
}
