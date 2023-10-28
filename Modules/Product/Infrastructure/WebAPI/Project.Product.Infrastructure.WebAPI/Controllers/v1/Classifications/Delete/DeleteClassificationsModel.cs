using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Delete
{
    public class DeleteClassificationsModel
    {
        public int? Id { get; set; }
    }
    public class DeleteClassificationsModelValidator : AbstractValidator<DeleteClassificationsModel>
    {
        public DeleteClassificationsModelValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteClassificationsModel.Id)} can not be empty");
        }
    }
}
