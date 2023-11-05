using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Post;
using Project.Product.Integration.Trademarks.Command;
using Project.Product.Integration.Trademarks.Query;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks
{
    public class TrademarkController : CommonController
    {
        private readonly IValidator<UpdateTrademarkRequestModel> trademarkValidator;
        public TrademarkController(ISender mediator, IValidator<UpdateTrademarkRequestModel> trademarkValidator) : base(mediator)
        {
            this.trademarkValidator = trademarkValidator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<TrademarkResponseModel>>> GetColors()
        {
            var result = await this.Mediator.Send(new GetTrademarkQuery());

            return new ResponseBaseModel<TrademarkResponseModel>
            {
                Data = result.Adapt<TrademarkResponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> UpdateColors(UpdateTrademarkRequestModel request)
        {
            var validator = await this.trademarkValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateTrademarkCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [AllowAnonymous]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandProductBase>> DeleteColors(DeleteTrademarkRequestModel request)
        {

            var command = request.Adapt<DeleteTrademarkCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [AllowAnonymous]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandProductBase>> ReactiveColors(DeleteTrademarkRequestModel request)
        {

            var command = request.Adapt<ReactiveTrademarkCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }
    }
}
