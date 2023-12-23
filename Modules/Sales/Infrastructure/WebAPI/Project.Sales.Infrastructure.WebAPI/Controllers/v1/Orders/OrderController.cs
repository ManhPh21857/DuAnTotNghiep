using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.Sales.Domain.Orders;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Post;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Put;
using Project.Sales.Integration.Orders.Command;
using Project.Sales.Integration.Orders.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders
{
    public class OrderController : SalesController
    {
        private readonly IValidator<CreateOrderRequestModel> validatorCreateOrderRequestModel;

        public OrderController(
            ISender mediator,
            IValidator<CreateOrderRequestModel> validatorCreateOrderRequestModel
        ) : base(mediator)
        {
            this.validatorCreateOrderRequestModel = validatorCreateOrderRequestModel;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> CreateOrder(
            CreateOrderRequestModel request
        )
        {
            var validator = await validatorCreateOrderRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<CreateOrderCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<GetOrderResponseModel>>> GetOrders()
        {
            var query = new GetOrderQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetOrderResponseModel>
            {
                Data = result.Adapt<GetOrderResponseModel>()
            };

            return response;
        }

        [HttpPut("cancel/{id}")]
        public async Task<ActionResult<ResponseBaseModel<CancelOrderResponseModel>>> CancelOrder(int id)
        {
            var command = new CancelOrderCommand(id);

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CancelOrderResponseModel>
            {
                Data = result.Adapt<CancelOrderResponseModel>()
            };

            return response;
        }

        [HttpPost("shop-order/{page}")]
        public async Task<ActionResult<ResponseBaseModel<GetShopOrderResponseModel>>> GetShopOrder(
            int page,
            [FromBody] GetShopOrderRequestModel? request
        )
        {
            var query = new GetShopOrderQuery(page, request?.Adapt<GetOrderFilter>());
                
            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetShopOrderResponseModel>
            {
                Data = result.Adapt<GetShopOrderResponseModel>()
            };

            return response;
        }

        [AllowAnonymous]
        [HttpGet("detail/{orderId}")]
        public async Task<ActionResult<ResponseBaseModel<GetOrderDetailResponseModel>>> GetOrderDetails(int orderId)
        {
            var query = new GetOrderDetailQuery(orderId);

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetOrderDetailResponseModel>
            {
                Data = result.Adapt<GetOrderDetailResponseModel>()
            };

            return response;
        }
    }
}
