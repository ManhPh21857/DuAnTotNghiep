using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Delete
{
    public class DeleteTrademarkModel
    {
        public int? Id { get; set; }
    }
    public class DeleteTrademarkModelValidator : AbstractValidator<DeleteTrademarkModel>
    {
        public DeleteTrademarkModelValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteTrademarkModel.Id)} can not be empty");
        }
    }
}
