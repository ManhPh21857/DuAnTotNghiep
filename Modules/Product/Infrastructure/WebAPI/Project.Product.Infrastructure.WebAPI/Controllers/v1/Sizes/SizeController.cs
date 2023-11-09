using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Enums;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Post;
using Project.Product.Integration.Sizes.Command;
using Project.Product.Integration.Sizes.Query;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes
{
    public class SizeController : CommonController
    {
        private readonly IValidator<UpdateSizeRequestModel> sizeValidator;
        public SizeController(ISender mediator, IValidator<UpdateSizeRequestModel> sizeValidator) : base(mediator)
        {
            this.sizeValidator = sizeValidator;
        }

        [Authorize(Roles = nameof(Role.SizeView))]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<SizeResponseModel>>> GetSizes()
        {
            var result = await this.Mediator.Send(new GetSizeQuery());

            return new ResponseBaseModel<SizeResponseModel>
            {
                Data = result.Adapt<SizeResponseModel>()
            };
        }

        [Authorize(Roles = nameof(Role.SizeEdit))]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> UpdateSizes(UpdateSizeRequestModel request)
        {
            var validator = await this.sizeValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateSizeCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.SizeDelete))]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandProductBase>> DeleteSizes(DeleteSizeRequestModel request)
        {

            var command = request.Adapt<DeleteSizeCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.SizeEdit))]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandProductBase>> ReactiveSizes(DeleteSizeRequestModel request)
        {

            var command = request.Adapt<ReactiveSizeCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }
    }
}
