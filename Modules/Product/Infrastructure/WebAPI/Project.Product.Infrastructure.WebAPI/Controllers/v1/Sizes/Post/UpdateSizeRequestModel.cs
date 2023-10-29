using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Post
{
    public class UpdateSizeRequestModel
    {
        public IEnumerable<UpdateSizeModel> Sizes { get; set; }

        public UpdateSizeRequestModel()
        {
            Sizes = new List<UpdateSizeModel>();
        }
    }
    public class UpdateSizeRequestModelValidator : AbstractValidator<UpdateSizeRequestModel>
    {
        public UpdateSizeRequestModelValidator(IValidator<UpdateSizeModel> updateSizeModelValidator)
        {
            this.RuleForEach(x => x.Sizes).SetValidator(updateSizeModelValidator);
        }
    }
}
