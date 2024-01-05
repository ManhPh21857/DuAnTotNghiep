using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Delete
{
    public class DeleteGroupRequestModel
    {
        public IEnumerable<DeleteGroupModel>? Groups { get; set; }
    }

    public class DeleteGroupRequestModelValidator : AbstractValidator<DeleteGroupRequestModel>
    {
        public DeleteGroupRequestModelValidator()
        {
            this.RuleFor(x => x.Groups).NotNull();
        }
    }
}
