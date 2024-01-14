using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get;
using Project.Sales.Integration.Dashboarts.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts
{
    public class DashboartController : SalesController
    {
        public DashboartController(ISender mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpGet("CustomerTotal")]
        public async Task<ActionResult<ResponseBaseModel<DashboartResponseModel>>> GetCustomerTotal()
        {

            var result = await Mediator.Send(new GetDashboartQuery());

            return new ResponseBaseModel<DashboartResponseModel>
            {
                Data = result.Adapt<DashboartResponseModel>()
            };

        }
        [AllowAnonymous]
        [HttpGet("ProductTotal")]
        public async Task<ActionResult<ResponseBaseModel<ProductTotalResponseModel>>> GetProductTotal()
        {

            var result = await Mediator.Send(new GetProductTotalQuery());

            return new ResponseBaseModel<ProductTotalResponseModel>
            {
                Data = result.Adapt<ProductTotalResponseModel>()
            };

        }

        [AllowAnonymous]
        [HttpGet("OrderTotal")]
        public async Task<ActionResult<ResponseBaseModel<OrderTotalModelResponseModel>>> GetOrderTotal()
        {

            var result = await Mediator.Send(new GetOrderTotalQuery());

            return new ResponseBaseModel<OrderTotalModelResponseModel>
            {
                Data = result.Adapt<OrderTotalModelResponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpGet("SoldOutProduct")]
        public async Task<ActionResult<ResponseBaseModel<SoldOutProductResponseModel>>> SoldOutProduct()
        {

            var result = await Mediator.Send(new GetSoldOutProductQuery());

            return new ResponseBaseModel<SoldOutProductResponseModel>
            {
                Data = result.Adapt<SoldOutProductResponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpGet("OrderUnconfimred")]
        public async Task<ActionResult<ResponseBaseModel<OrderUnconfimredResponseModel>>> OrderUnconfimred()
        {

            var result = await Mediator.Send(new GetOrderUnConfimredQuery());

            return new ResponseBaseModel<OrderUnconfimredResponseModel>
            {
                Data = result.Adapt<OrderUnconfimredResponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpGet("NewCustomer")]
        public async Task<ActionResult<ResponseBaseModel<GetNewCustomerQueryResult>>> NewCustomer()
        {

            var result = await Mediator.Send(new GetNewCustomerQuery());

            return new ResponseBaseModel<GetNewCustomerQueryResult>
            {
                Data = result.Adapt<GetNewCustomerQueryResult>()
            };
        }
    }
}
