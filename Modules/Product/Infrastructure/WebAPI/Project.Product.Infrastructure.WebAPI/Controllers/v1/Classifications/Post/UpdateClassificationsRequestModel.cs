using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Post
{
    public class UpdateClassificationsRequestModel
    {
        public IEnumerable<UpdateClassificationsModel> Classifications { get; set; }

        public UpdateClassificationsRequestModel()
        {
            Classifications = new List<UpdateClassificationsModel>();
        }
    }
    public class UpdateClassificationsRequestModelValidator : AbstractValidator<UpdateClassificationsRequestModel>
    {
        public UpdateClassificationsRequestModelValidator(IValidator<UpdateClassificationsModel> updateClassificationsModelValidator)
        {
            this.RuleForEach(x => x.Classifications).SetValidator(updateClassificationsModelValidator);
        }
    }
}
