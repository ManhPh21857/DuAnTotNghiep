using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Put
{
    public class DeleteProductRequestModel
    {
        public IEnumerable<DeleteProductModel> Products { get; set; }

        public DeleteProductRequestModel()
        {
            this.Products = new List<DeleteProductModel>();
        }
    }

    public class DeleteProductRequestModelValidator : AbstractValidator<DeleteProductRequestModel>
    {
        public DeleteProductRequestModelValidator(IValidator<DeleteProductModel> validator)
        {
            this.RuleForEach(x => x.Products).SetValidator(validator);
        }
    }
}
