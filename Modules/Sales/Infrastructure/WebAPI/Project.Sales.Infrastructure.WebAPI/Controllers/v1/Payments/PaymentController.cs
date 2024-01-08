using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Payments.Get;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Payments.Post;
using Project.Sales.Integration.Payments.Command;
using Project.Sales.Integration.Payments.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Payments
{
    public class PaymentController : SalesController
    {
        public PaymentController(ISender mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("order-finish")]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> UpdateOrder(
            FinishOrderRequestModel request
        )
        {
            var command = request.Adapt<FinishOrderCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<GetPaymentMethodResponseModel>>> GetPaymentMethods()
        {
            var query = new GetPaymentMethodQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetPaymentMethodResponseModel>
            {
                Data = result.Adapt<GetPaymentMethodResponseModel>()
            };

            return response;
        }

        [HttpPost("continue")]
        public async Task<ActionResult<ResponseBaseModel<ContinuePaySessionResponseModel>>> ContinuePaySession(
            ContinuePaySessionRequestModel request
        )
        {
            var command = request.Adapt<CreateMoMoPaymentCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<ContinuePaySessionResponseModel>
            {
                Data = new ContinuePaySessionResponseModel
                {
                    PayUrl = result.PayUrl
                }
            };

            return response;
        }
    }
}
