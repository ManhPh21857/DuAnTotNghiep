using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Delete
{
    public class DeleteMaterialsModel
    {
        public int? Id { get; set; }
    }
    public class DeleteMaterialsModelValidator : AbstractValidator<DeleteMaterialsModel>
    {
        public DeleteMaterialsModelValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteMaterialsModel.Id)} can not be empty");
        }
    }
}
