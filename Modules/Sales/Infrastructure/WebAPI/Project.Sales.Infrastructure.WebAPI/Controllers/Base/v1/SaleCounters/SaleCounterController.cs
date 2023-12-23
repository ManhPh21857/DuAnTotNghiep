using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post;
using Project.Sales.Integration.SaleCounters.Command;
using Project.Sales.Integration.SaleCounters.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters
{
    public class SaleCounterController : SalesController
    {
        public SaleCounterController(ISender mediator) : base(mediator)
        {

        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ActionResult<ResponseBaseModel<GetSaleCounterResponseModel>>> GetSaleCounter()
        {

            var result = await Mediator.Send(new GetSaleCounterQuery());

            return new ResponseBaseModel<GetSaleCounterResponseModel>
            {
                Data = result.Adapt<GetSaleCounterResponseModel>()
            };

        }

        [AllowAnonymous]
        [HttpGet("productdetailid/{productId}/{colorId}/{sizeId}")]
        public async Task<ActionResult<ResponseBaseModel<ProductDetailIdResponseModel>>> GetSaleCounterId(int productId, int colorId, int sizeId)
        {

            var query = new GetProductDetailIdQuery(productId, colorId, sizeId);

            var result = await Mediator.Send(query);


            return new ResponseBaseModel<ProductDetailIdResponseModel>
            {
                Data = result.Adapt<ProductDetailIdResponseModel>()
            };

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ResponseBaseModel<CommandSalesBase>> CreateSaleOrderDetail([FromBody] CreateSaleOrderDetailModel request)
        {
            var command = request.Adapt<CreateOrderDetailCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };
        }
       
    }
}
