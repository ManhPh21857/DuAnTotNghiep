using FluentValidation;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Get;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Post
{
    public class UpdateColorRequestModel
    {
        public IEnumerable<UpdateColorModel> Colors { get; set; }

        public UpdateColorRequestModel()
        {
            Colors = new List<UpdateColorModel>();
        }
    }

    public class UpdateColorRequestModelValidator : AbstractValidator<UpdateColorRequestModel>
    {
        public UpdateColorRequestModelValidator(IValidator<UpdateColorModel> updateColorModelValidator)
        {
            this.RuleForEach(x => x.Colors).SetValidator(updateColorModelValidator);
        }
    }
}
