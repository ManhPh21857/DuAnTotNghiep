using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Enums;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Post;
using Project.Product.Integration.Colors.Command;
using Project.Product.Integration.Colors.Query;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors
{
    public class ColorController : CommonController
    {
        private readonly IValidator<UpdateColorRequestModel> colorValidator;
        public ColorController(ISender mediator, IValidator<UpdateColorRequestModel> colorValidator) : base(mediator)
        {
            this.colorValidator = colorValidator;
        }

        [Authorize(Roles = nameof(Role.ColorView))]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<ColorResponseModel>>> GetColors()
        {
            var result = await this.Mediator.Send(new GetColorQuery());

            return new ResponseBaseModel<ColorResponseModel>
            {
                Data = result.Adapt<ColorResponseModel>()
            };
        }

        [Authorize(Roles = nameof(Role.ColorEdit))]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> UpdateColors(UpdateColorRequestModel request)
        {
            var validator = await this.colorValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateColorCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.ColorDelete))]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandProductBase>> DeleteColors(DeleteColorRequestModel request)
        {

            var command = request.Adapt<DeleteColorCommand>();
            
            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.ColorEdit))]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandProductBase>> ReactiveColors(DeleteColorRequestModel request)
        {

            var command = request.Adapt<ReactiveColorCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }
    }
}
