using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Put
{
    public class UpdateSupplierModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? AddressID { get; set; }
        public int? Status { get; set; }
    }
    public class UpdateSupplierModelValidator : AbstractValidator<UpdateSupplierModel>
    {
        public UpdateSupplierModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateSupplierModel.Name)} can not be empty");


            RuleFor(v => v.Status)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateSupplierModel.Status)} can not be empty");

        }
    }
}
