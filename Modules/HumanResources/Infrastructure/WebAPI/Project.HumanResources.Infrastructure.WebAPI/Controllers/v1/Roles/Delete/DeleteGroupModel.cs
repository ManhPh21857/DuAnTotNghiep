using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Delete
{
    public class DeleteGroupModel
    {
        public int? Id { get; set; }
        public byte[]? DataVersion { get; set; }
    }
    public class DeleteGroupModelValidator : AbstractValidator<DeleteGroupModel>
    {
        public DeleteGroupModelValidator()
        {
            this.RuleFor(x => x.Id).NotNull();
        }
    }
}
