using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Post
{
    public class UpdateMaterialModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public byte[]? DataVersion { get; set; }
    }
    public class UpdateMaterialModelValidator : AbstractValidator<UpdateMaterialModel>
    {
        public UpdateMaterialModelValidator()
        {
            this.RuleFor(x => x.Name)
               .NotNull()
               .WithMessage($"{nameof(UpdateMaterialModel.Name)} must have a value")
               .NotEmpty()
               .WithMessage($"{nameof(UpdateMaterialModel.Name)} must have a value");
        }
    }
}
