using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Post
{
    public class CreateSupplierModel
    {

        public string? Name { get; set; }
        public string? Address { get; set; }

    }
    public class CreateSupplierModelValidator : AbstractValidator<CreateSupplierModel>
    {
        public CreateSupplierModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(CreateSupplierModel.Name)} can not be empty");


            RuleFor(v => v.Address)
            .NotEmpty()
            .WithMessage($"{nameof(CreateSupplierModel.Address)} can not be empty");
        }
    }
}
