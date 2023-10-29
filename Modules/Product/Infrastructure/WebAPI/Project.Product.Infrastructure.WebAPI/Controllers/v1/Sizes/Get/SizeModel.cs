

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Get
{
    public class SizeModel
    {
        public int? Id { get; set; }
        public string? Size { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
