namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class SoldOutProductDetailResponseModel
    {
        public IEnumerable<SoldOutProductDetailModel> SoldOutProductDetails { get; set; }

        public SoldOutProductDetailResponseModel(IEnumerable<SoldOutProductDetailModel> soldOutProductDetails)
        {
            SoldOutProductDetails = soldOutProductDetails;
        }
    }
}
