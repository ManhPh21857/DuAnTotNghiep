using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Post
{
    public class CreateMaterialsModel
    {
        public string? Name { get; set; }
    }
    public class CreateMaterialsModelValidator : AbstractValidator<CreateMaterialsModel>
    {
        public CreateMaterialsModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(CreateMaterialsModel.Name)} can not be empty");
        }
    }
}
