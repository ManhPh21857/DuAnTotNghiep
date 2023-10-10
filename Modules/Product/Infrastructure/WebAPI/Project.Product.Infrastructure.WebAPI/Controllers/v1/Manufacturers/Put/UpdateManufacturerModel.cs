using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers.Put
{
    public class UpdateManufacturerModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
    }
    public class UpdateManufacturerModelValidator : AbstractValidator<UpdateManufacturerModel>
    {
        public UpdateManufacturerModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateManufacturerModel.Name)} can not be empty");


            RuleFor(v => v.Status)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateManufacturerModel.Status)} can not be empty");

        }
    }
}
