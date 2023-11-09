using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Enums;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Post;
using Project.Product.Integration.Origins.Command;
using Project.Product.Integration.Origins.Query;


namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins
{
    public class OriginController : CommonController
    {
        private readonly IValidator<UpdateOriginRequestModel> originValidator;
        public OriginController(ISender mediator, IValidator<UpdateOriginRequestModel> originValidator) : base(mediator)
        {
            this.originValidator = originValidator;
        }

        [Authorize(Roles = nameof(Role.OriginView))]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<OriginResponseModel>>> GetOrigins()
        {
            var result = await this.Mediator.Send(new GetOriginQuery());

            return new ResponseBaseModel<OriginResponseModel>
            {
                Data = result.Adapt<OriginResponseModel>()
            };
        }

        [Authorize(Roles = nameof(Role.OriginEdit))]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> UpdateOrigin(UpdateOriginRequestModel request)
        {
            var validator = await this.originValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateOriginCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.OriginDelete))]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandProductBase>> DeleteOrigin(DeleteOriginRequestModel request)
        {

            var command = request.Adapt<DeleteOriginCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.OriginEdit))]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandProductBase>> ReactiveOrigin(DeleteOriginRequestModel request)
        {

            var command = request.Adapt<ReactiveOriginCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }
    }
}
