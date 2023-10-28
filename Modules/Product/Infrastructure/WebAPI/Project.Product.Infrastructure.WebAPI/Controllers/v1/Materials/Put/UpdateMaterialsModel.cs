using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Put
{
    public class UpdateMaterialsModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
    public class UpdateMaterialsModelValidator : AbstractValidator<UpdateMaterialsModel>
    {
        public UpdateMaterialsModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateMaterialsModel.Name)} can not be empty")
                .NotNull()
                .WithMessage($"{nameof(UpdateMaterialsModel.Name)} can not null");

            RuleFor(v => v.Id)
                .NotNull()
                .WithMessage($"{nameof(UpdateMaterialsModel.Id)} can not null");

        }
    }
}
