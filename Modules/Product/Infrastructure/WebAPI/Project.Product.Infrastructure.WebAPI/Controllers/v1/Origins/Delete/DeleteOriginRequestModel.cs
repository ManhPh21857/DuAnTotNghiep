namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Delete
{
    public class DeleteOriginRequestModel
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
