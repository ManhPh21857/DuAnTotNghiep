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
        public int? AddressID { get; set; }
        public int? Status { get; set; }

    }
    public class CreateSupplierModelValidator : AbstractValidator<CreateSupplierModel>
    {
        public CreateSupplierModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(CreateSupplierModel.Name)} can not be empty");


            RuleFor(v => v.Status)
                .NotEmpty()
                .WithMessage($"{nameof(CreateSupplierModel.Status)} can not be empty");

            RuleFor(v => v.AddressID)
            .NotEmpty()
            .WithMessage($"{nameof(CreateSupplierModel.AddressID)} can not be empty");
        }
    }
}
