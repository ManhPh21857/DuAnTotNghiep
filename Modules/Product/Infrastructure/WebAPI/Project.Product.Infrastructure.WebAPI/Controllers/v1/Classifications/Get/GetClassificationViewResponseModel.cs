namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Get
{
    public class GetClassificationViewResponseModel
    {
        public IEnumerable<GetClassificationViewModel> Classifications { get; set; }
        public GetClassificationViewResponseModel()
        {
            this.Classifications = new List<GetClassificationViewModel>();
        }
    }
}
