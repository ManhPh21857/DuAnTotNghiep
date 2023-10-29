using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Post
{
    public class UpdateOriginModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public byte[]? DataVersion { get; set; }
    }
    public class UpdateOriginModelValidator : AbstractValidator<UpdateOriginModel>
    {
        public UpdateOriginModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateOriginModel.Name)} must have a value")
                .NotNull()
                .WithMessage($"{nameof(UpdateOriginModel.Name)} must have a value");
        }
    }
}
