
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Post
{
    public class UpdateTrademarkRequestModel
    {
        public IEnumerable<UpdateTrademarkModel> Trademarks { get; set; }

        public UpdateTrademarkRequestModel()
        {
            Trademarks = new List<UpdateTrademarkModel>();
        }
    }

    public class UpdateTrademarkRequestModelValidator : AbstractValidator<UpdateTrademarkRequestModel>
    {
        public UpdateTrademarkRequestModelValidator(IValidator<UpdateTrademarkModel> updateTrademarkModelValidator)
        {
            this.RuleForEach(x => x.Trademarks).SetValidator(updateTrademarkModelValidator);
        }
    }
}
