using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Get
{
    public class ColorModel
    {
        public int? Id { get; set; }
        public string? Color { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}