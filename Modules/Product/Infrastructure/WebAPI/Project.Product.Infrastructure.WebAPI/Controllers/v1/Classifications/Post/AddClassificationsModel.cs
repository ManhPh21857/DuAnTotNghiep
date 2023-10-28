using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Post
{
    public class AddClassificationsModel
    {
        public string? Name { get; set; }
    }
    public class AddClassificationsModelValidator : AbstractValidator<AddClassificationsModel>
    {
        public AddClassificationsModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(AddClassificationsModel.Name)} can not be empty");
        }
    }
}
