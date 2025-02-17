﻿using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Post
{
    public class UpdateMaterialModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public byte[]? DataVersion { get; set; }
    }
    public class UpdateMaterialModelValidator : AbstractValidator<UpdateMaterialModel>
    {
        public UpdateMaterialModelValidator()
        {
            this.RuleFor(x => x.Name)
               .NotNull()
               .WithMessage($"{nameof(UpdateMaterialModel.Name)} must have a value")
               .NotEmpty()
               .WithMessage($"{nameof(UpdateMaterialModel.Name)} must have a value")
               .MaximumLength(200);
        }
    }
}
