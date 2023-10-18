using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Post
{
    public class CreateOriginModel
    {
        public string? Name { get; set; }
    }
    public class CreateOriginModelValidator : AbstractValidator<CreateOriginModel>
    {
        public CreateOriginModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(CreateOriginModel.Name)} can not be empty");
        }
    }
}
