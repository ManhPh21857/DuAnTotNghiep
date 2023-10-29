using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<SizeResponseModel>>> GetSizes()
        {
            var result = await this.Mediator.Send(new GetSizeQuery());

            return new ResponseBaseModel<SizeResponseModel>
            {
                Data = result.Adapt<SizeResponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandBaseModel>>> UpdateSizes(UpdateSizeRequestModel request)
        {
            var validator = await this.sizeValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateSizeCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandBaseModel>> DeleteSizes(DeleteSizeRequestModel request)
        {

            var command = request.Adapt<DeleteSizeCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandBaseModel>> ReactiveSizes(DeleteSizeRequestModel request)
        {

            var command = request.Adapt<ReactiveSizeCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }
    }
}
