using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Enums;
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

        [Authorize(Roles = nameof(Role.TrademarkView))]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<TrademarkResponseModel>>> GetTrademark()
        {
            var result = await this.Mediator.Send(new GetTrademarkQuery());

            return new ResponseBaseModel<TrademarkResponseModel>
            {
                Data = result.Adapt<TrademarkResponseModel>()
            };
        }

        [Authorize(Roles = nameof(Role.TrademarkEdit))]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> UpdateTrademark(UpdateTrademarkRequestModel request)
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

        [Authorize(Roles = nameof(Role.TrademarkDelete))]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandProductBase>> DeleteTrademark(DeleteTrademarkRequestModel request)
        {

            var command = request.Adapt<DeleteTrademarkCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.TrademarkEdit))]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandProductBase>> ReactiveTrademark(DeleteTrademarkRequestModel request)
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
