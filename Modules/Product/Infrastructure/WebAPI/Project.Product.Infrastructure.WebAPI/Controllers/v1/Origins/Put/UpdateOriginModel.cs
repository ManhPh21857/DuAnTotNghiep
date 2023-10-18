using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Put
{
    public class UpdateOriginModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
    public class UpdateOriginModelValidator : AbstractValidator<UpdateOriginModel>
    {
        public UpdateOriginModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateOriginModel.Name)} can not be empty");


            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateOriginModel.Id)} can not be empty");

        }
    }
}
