using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers1.Post
{
    public class CreateManufacturerModel
    {

        public string? Name { get; set; }
        public int? Status { get; set; }
    }
    public class CreateManufacturerModelValidator : AbstractValidator<CreateManufacturerModel>
    {
        public CreateManufacturerModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(CreateManufacturerModel.Name)} can not be empty");


            RuleFor(v => v.Status)
                .NotEmpty()
                .WithMessage($"{nameof(CreateManufacturerModel.Status)} can not be empty");
               
        }
    }
}
