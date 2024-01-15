namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class SoldOutProductDetailModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int ActualQuantity { get; set; }
        public float Price { get; set; }
    }
}
