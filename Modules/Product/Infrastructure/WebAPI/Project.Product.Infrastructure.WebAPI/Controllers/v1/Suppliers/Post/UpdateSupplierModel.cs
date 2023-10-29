using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Post
{
    public class UpdateSupplierModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public byte[]? DataVersion { get; set; }

    }
    public class UpdateSupplierModelValidator : AbstractValidator<UpdateSupplierModel>
    {
        public UpdateSupplierModelValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .WithMessage($"{nameof(UpdateSupplierModel.Name)} must have a value")
                .NotEmpty()
                .WithMessage($"{nameof(UpdateSupplierModel.Name)} must have a value");


            RuleFor(v => v.Address)
                .NotNull()
                .WithMessage($"{nameof(UpdateSupplierModel.Address)} must have a value")
                .NotEmpty()
                .WithMessage($"{nameof(UpdateSupplierModel.Address)} must have a value");
        }
    }
}
