using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Post
{
    public class UpdateGroupRoleRequestModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? DataVersion { get; set; }
        public List<int>? Roles { get; set; }
    }

    public class UpdateGroupRoleRequestModelValidator : AbstractValidator<UpdateGroupRoleRequestModel>
    {
        public UpdateGroupRoleRequestModelValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateGroupRoleRequestModel.Name)} không thể trống")
                .MaximumLength(50);

            this.RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateGroupRoleRequestModel.Description)} không thể trống")
                .MaximumLength(200);

            this.RuleFor(x => x.Roles)
                .NotNull()
                .WithMessage($"{nameof(UpdateGroupRoleRequestModel.Roles)} không thể trống");
        }
    }
}
