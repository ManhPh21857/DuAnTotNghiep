﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Delete
{
    public class DeleteOriginModel
    {
        public int? Id { get; set; }
    }
    public class DeleteOriginModelValidator : AbstractValidator<DeleteOriginModel>
    {
        public DeleteOriginModelValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteOriginModel.Id)} can not be empty");
        }
    }
}
