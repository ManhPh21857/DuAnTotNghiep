using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Put;
using Project.Product.Integration.CartDetails.Command;
using Project.Product.Integration.CartDetails.Query;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails
{
    public class CartdetailController : CommonController
    {
        private readonly IValidator<CreateCartdetailModel> createCartdetailValidator;
        private readonly IValidator<UpdateCartdetailModel> updateCartdetailValidator;
        private readonly IValidator<DeleteCartdetailModel> deleteCartdetailValidator;
        public CartdetailController(ISender meadiator,
            IValidator<CreateCartdetailModel> createCartdetailValidator,
            IValidator<UpdateCartdetailModel> updateCartdetailValidator,
            IValidator<DeleteCartdetailModel> deleteCartdetailValidator
            ) : base(meadiator)
        {
            this.createCartdetailValidator = createCartdetailValidator;
            this.updateCartdetailValidator = updateCartdetailValidator;
            this.deleteCartdetailValidator = deleteCartdetailValidator;
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ResponseBaseModel<GetCartdetailReponseModel>> GetCartdetail()
        {
            var result = await Mediator.Send(new GetCartdetaiQuery());

            return new ResponseBaseModel<GetCartdetailReponseModel>
            {
                Data = result.Adapt<GetCartdetailReponseModel>()
            };
        }
        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<ResponseBaseModel<CreateCartdetailResponseModel>>> CreateCartdetail(
       [FromBody] CreateCartdetailModel request)
        {
            var validator = await createCartdetailValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<CreateCartdetailCommand>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<CreateCartdetailResponseModel>
            {
                Data = result.Adapt<CreateCartdetailResponseModel>()
            };

            return response;
        }



        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ActionResult<ResponseBaseModel<UpdateCartdetailResponseModel>>> UpdateCartdetail(
       [FromBody] UpdateCartdetailModel request)
        {
            var validator = await updateCartdetailValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<UpdateCartdetailCommand>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<UpdateCartdetailResponseModel>
            {
                Data = result.Adapt<UpdateCartdetailResponseModel>()
            };

            return response;
        }


        [AllowAnonymous]
        [HttpDelete("{cart_id}/{product_detail_id}")]
        public async Task<ActionResult<ResponseBaseModel<DeleteCartdetailResponseModel>>> DeleteCartdetail(int cart_id,int product_detail_id)
        {
            var registerRequest = new DeleteCartdetailCommand(cart_id, product_detail_id);


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<DeleteCartdetailResponseModel>
            {
                Data = result.Adapt<DeleteCartdetailResponseModel>()
            };

            return response;
        }
    }
}
