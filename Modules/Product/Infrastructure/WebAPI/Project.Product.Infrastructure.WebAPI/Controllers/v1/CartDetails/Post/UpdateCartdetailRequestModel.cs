
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post
{
    public class UpdateCartdetailRequestModel
    {
        public IEnumerable<UpdateCartdetailModel> CartDetails { get; set; }

        public UpdateCartdetailRequestModel()
        {
            CartDetails = new List<UpdateCartdetailModel>();
        }
        
    }

	public class UpdateCartdetailRequestModelValidator : AbstractValidator<UpdateCartdetailRequestModel>
	{
		public UpdateCartdetailRequestModelValidator(IValidator<UpdateCartdetailModel> updateCartdetailModelValidator)
		{
			this.RuleForEach(x => x.CartDetails).SetValidator(updateCartdetailModelValidator);
		}
	}
}
