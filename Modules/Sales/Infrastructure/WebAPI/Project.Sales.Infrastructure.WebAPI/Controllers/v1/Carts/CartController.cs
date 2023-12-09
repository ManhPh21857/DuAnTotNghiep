using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Get;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Post;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Put;
using Project.Sales.Integration.Carts.Command;
using Project.Sales.Integration.Carts.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts
{
    public class CartController : SalesController
    {
        private readonly IValidator<CartAdditionRequestModel> validatorCartAdditionRequestModel;
        private readonly IValidator<DeleteCartDetailRequestModel> validatorDeleteCartDetailRequestModel;
        private readonly IValidator<UpdateCartDetailRequestModel> validatorUpdateCartDetailRequestModel;

        public CartController(
            ISender mediator,
            IValidator<CartAdditionRequestModel> validatorCartAdditionRequestModel,
            IValidator<DeleteCartDetailRequestModel> validatorDeleteCartDetailRequestModel,
            IValidator<UpdateCartDetailRequestModel> validatorUpdateCartDetailRequestModel
        ) : base(mediator)
        {
            this.validatorCartAdditionRequestModel = validatorCartAdditionRequestModel;
            this.validatorDeleteCartDetailRequestModel = validatorDeleteCartDetailRequestModel;
            this.validatorUpdateCartDetailRequestModel = validatorUpdateCartDetailRequestModel;
        }

        [HttpGet("count")]
        public async Task<ActionResult<ResponseBaseModel<CartItemCount>>> GetCartCount()
        {
            var result = await this.Mediator.Send(new GetCountItemQuery());

            return new ResponseBaseModel<CartItemCount>
            {
                Data = result.Adapt<CartItemCount>()
            };
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<CartDetailResponseModel>>> GetCartDetail()
        {
            var result = await this.Mediator.Send(new GetCartDetailQuery());

            return new ResponseBaseModel<CartDetailResponseModel>
            {
                Data = result.Adapt<CartDetailResponseModel>()
            };
        }

        [HttpPost("addition")]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> AddToCart(CartAdditionRequestModel request)
        {
            var validator = await this.validatorCartAdditionRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<AddToCartCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpPut("delete")]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> DeleteCartDetails(
            DeleteCartDetailRequestModel request
        )
        {
            var validator = await this.validatorDeleteCartDetailRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<DeleteCartDetailCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpPut]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> UpdateCartDetail(
            UpdateCartDetailRequestModel request
        )
        {
            var validator = await this.validatorUpdateCartDetailRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<UpdateCartDetailCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }
    }
}
