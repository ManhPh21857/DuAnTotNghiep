using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Put;
using Project.Product.Integration.Origins.Command;
using Project.Product.Integration.Origins.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins
{
    public class OriginController : CommonController
    {
        private readonly IValidator<CreateOriginModel> createOriginValidator;
        private readonly IValidator<UpdateOriginModel> updateOriginValidator;
        private readonly IValidator<DeleteOriginModel> deleteOriginValidator;
        public OriginController(ISender meadiator,
             IValidator<CreateOriginModel> createOriginValidator,
             IValidator<UpdateOriginModel> updateOriginValidator,
             IValidator<DeleteOriginModel> deleteOriginValidator
            ) : base(meadiator)
        {
            this.createOriginValidator = createOriginValidator;
            this.updateOriginValidator = updateOriginValidator;
            this.deleteOriginValidator = deleteOriginValidator;
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ResponseBaseModel<GetOriginReponseModel>> GetColors()
        {
            var result = await Mediator.Send(new GetOriginQuery());

            return new ResponseBaseModel<GetOriginReponseModel>
            {
                Data = result.Adapt<GetOriginReponseModel>()
            };
        }



        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<ResponseBaseModel<CreateOriginReponseModel>>> CreateManufacturers(
       [FromBody] CreateOriginModel request)
        {
            var validator = await createOriginValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<CreateOriginQuery>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<CreateOriginReponseModel>
            {
                Data = result.Adapt<CreateOriginReponseModel>()
            };

            return response;
        }



        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ActionResult<ResponseBaseModel<UpdateOriginReponseModel>>> UpdateManufacturers(
       [FromBody] UpdateOriginModel request)
        {
            var validator = await updateOriginValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<UpdateOriginQuery>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<UpdateOriginReponseModel>
            {
                Data = result.Adapt<UpdateOriginReponseModel>()
            };

            return response;
        }


        [AllowAnonymous]
        [HttpDelete("")]
        public async Task<ActionResult<ResponseBaseModel<DeleteOriginReponseModel>>> DeleteManufacturers(
     [FromBody] DeleteOriginModel request)
        {
            var validator = await deleteOriginValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<DeleteOriginQuery>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<DeleteOriginReponseModel>
            {
                Data = result.Adapt<DeleteOriginReponseModel>()
            };

            return response;
        }
    }
}
