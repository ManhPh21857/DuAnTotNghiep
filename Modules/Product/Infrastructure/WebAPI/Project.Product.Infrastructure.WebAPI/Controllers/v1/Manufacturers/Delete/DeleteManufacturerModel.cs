using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers.Delete
{
    public class DeleteManufacturerModel
    {
        public int? Id { get; set; }
    }
    public class DeleteManufacturerModelValidator : AbstractValidator<DeleteManufacturerModel>
    {
        public DeleteManufacturerModelValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteManufacturerModel.Id)} can not be empty");
        }
    }
}
