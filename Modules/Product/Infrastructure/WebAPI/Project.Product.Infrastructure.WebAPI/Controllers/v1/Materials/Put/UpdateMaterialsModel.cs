using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Put
{
    public class UpdateMaterialsModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
    public class UpdateMaterialsModelValidator : AbstractValidator<UpdateMaterialsModel>
    {
        public UpdateMaterialsModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateMaterialsModel.Name)} can not be empty");


            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateMaterialsModel.Id)} can not be empty");

        }
    }
}
