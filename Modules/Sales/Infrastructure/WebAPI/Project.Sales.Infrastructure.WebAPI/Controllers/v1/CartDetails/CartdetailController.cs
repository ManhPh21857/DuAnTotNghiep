using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Delete;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post;
using Project.Sales.Integration.CartDetails.Command;
using Project.Sales.Integration.CartDetails.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails
{
    public class CartdetailController : SalesController
    {
        private readonly IValidator<UpdateCartdetailRequestModel> cartdetailValidator;
        public CartdetailController(ISender mediator, IValidator<UpdateCartdetailRequestModel> cartdetailValidator) : base(mediator)
        {
            this.cartdetailValidator = cartdetailValidator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<CartdetailResponseModel>>> GetCartdetail()
        {
            var result = await this.Mediator.Send(new GetCartdetailQuery());

            return new ResponseBaseModel<CartdetailResponseModel>
            {
                Data = result.Adapt<CartdetailResponseModel>()
            };
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> UpdateCartdetail(UpdateCartdetailRequestModel request)
        {
            var validator = await this.cartdetailValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateCartdetailCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };
        }

        [AllowAnonymous]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandSalesBase>> DeleteCartdetail([FromBody] DeleteCartdetailRequestModel request)
        {

            var command = request.Adapt<DeleteCartdetailCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };
        }


    }
}
