using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Post
{
    public class CreateTrademarkModel
    {
        public string? Name { get; set; }
    }
    public class CreateTrademarkModelValidator : AbstractValidator<CreateTrademarkModel>
    {
        public CreateTrademarkModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(CreateTrademarkModel.Name)} can not be empty");
        }
    }
}
