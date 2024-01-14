namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class SoldOutProductResponseModel
    {
        public IEnumerable<SoldOutProductModel> SoldOutProducts { get; set; }

        public SoldOutProductResponseModel(IEnumerable<SoldOutProductModel> soldOutProducts)
        {
            SoldOutProducts = soldOutProducts;
        }
    }
}
