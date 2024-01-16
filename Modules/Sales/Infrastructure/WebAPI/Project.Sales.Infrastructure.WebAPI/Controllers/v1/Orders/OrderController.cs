using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
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
        private readonly IValidator<AssignOrderRequestModel> validatorAssignOrderRequestModel;
        private readonly IValidator<FinishPrepareRequestModel> validatorFinishPrepareRequestModel;

        public OrderController(
            ISender mediator,
            IValidator<CreateOrderRequestModel> validatorCreateOrderRequestModel,
            IValidator<AssignOrderRequestModel> validatorAssignOrderRequestModel,
            IValidator<FinishPrepareRequestModel> validatorFinishPrepareRequestModel
        ) : base(mediator)
        {
            this.validatorCreateOrderRequestModel = validatorCreateOrderRequestModel;
            this.validatorAssignOrderRequestModel = validatorAssignOrderRequestModel;
            this.validatorFinishPrepareRequestModel = validatorFinishPrepareRequestModel;
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

        [AllowAnonymous]
        [HttpPut("cancel/{id}")]
        public async Task<ActionResult<ResponseBaseModel<CancelOrderResponseModel>>> CancelOrder(int id)
        {
            var command = new CancelOrderCommand(id, false);

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CancelOrderResponseModel>
            {
                Data = result.Adapt<CancelOrderResponseModel>()
            };

            return response;
        }

        [AllowAnonymous]
        [HttpPut("forced-cancel/{id}")]
        public async Task<ActionResult<ResponseBaseModel<CancelOrderResponseModel>>> ForcedCancelOrder(int id)
        {
            var command = new CancelOrderCommand(id, true);

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CancelOrderResponseModel>
            {
                Data = result.Adapt<CancelOrderResponseModel>()
            };

            return response;
        }

        [HttpGet("shop-order/{page}/{mode}")]
        public async Task<ActionResult<ResponseBaseModel<GetShopOrderResponseModel>>> GetShopOrder(
            int page,
            int mode
        )
        {
            var query = new GetShopOrderQuery(page, mode);

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetShopOrderResponseModel>
            {
                Data = result.Adapt<GetShopOrderResponseModel>()
            };

            return response;
        }

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

        [Authorize(Roles = nameof(Role.OrderEdit))]
        [HttpPut("assignment")]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> AssignOrder(
            AssignOrderRequestModel request
        )
        {
            var validator = await this.validatorAssignOrderRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<AssignOrderCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [Authorize(Roles = nameof(Role.OrderEdit))]
        [HttpPut("prepare-completion")]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> FinishPrepare(
            FinishPrepareRequestModel request
        )
        {
            var validator = await this.validatorFinishPrepareRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<FinishPrepareCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpPut("completion/{id}")]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> CompleteOrder(int id)
        {
            var command = new ReceivedOrderCommand(id);

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpGet("revenue")]
        public async Task<ActionResult<ResponseBaseModel<GetOrderRevenueResponseModel>>> GetOrderRevenue()
        {
            var query = new GetOrderRevenueQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetOrderRevenueResponseModel>
            {
                Data = result.Adapt<GetOrderRevenueResponseModel>()
            };

            return response;
        }
    }
}
