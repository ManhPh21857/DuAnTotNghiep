
using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post
{
    public class UpdateCartdetailRequestModel
    {
        public IEnumerable<UpdateCartdetailModel> Cartdetails { get; set; }

        public UpdateCartdetailRequestModel()
        {
            Cartdetails = new List<UpdateCartdetailModel>();
        }
        
    }

	public class UpdateCartdetailRequestModelValidator : AbstractValidator<UpdateCartdetailRequestModel>
	{
		public UpdateCartdetailRequestModelValidator(IValidator<UpdateCartdetailModel> updateCartdetailModelValidator)
		{
			this.RuleForEach(x => x.Cartdetails).SetValidator(updateCartdetailModelValidator);
		}
	}
}
