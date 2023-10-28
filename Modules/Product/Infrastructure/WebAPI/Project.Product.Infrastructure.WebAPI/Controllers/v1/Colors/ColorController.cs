using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<ColorResponseModel>>> GetColors()
        {
            var result = await this.Mediator.Send(new GetColorQuery());

            return new ResponseBaseModel<ColorResponseModel>
            {
                Data = result.Adapt<ColorResponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandBaseModel>>> UpdateColors(UpdateColorRequestModel request)
        {
            var validator = await this.colorValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateColorCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandBaseModel>> DeleteColors(DeleteColorRequestModel request)
        {

            var command = request.Adapt<DeleteColorCommand>();
            
            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandBaseModel>> ReactiveColors(DeleteColorRequestModel request)
        {

            var command = request.Adapt<ReactiveColorCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }
    }
}
