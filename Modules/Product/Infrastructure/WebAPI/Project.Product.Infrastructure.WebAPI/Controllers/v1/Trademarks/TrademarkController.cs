using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Put;
using Project.Product.Integration.Trademarks.Command;
using Project.Product.Integration.Trademarks.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks
{
    public class TrademarkController : CommonController
    {
        private readonly IValidator<CreateTrademarkModel> createTrademarkValidator;
        private readonly IValidator<UpdateTrademarkModel> updateTrademarkValidator;
        private readonly IValidator<DeleteTrademarkModel> deleteTrademarkValidator;
        public TrademarkController(ISender meadiator,
            IValidator<CreateTrademarkModel> createTrademarkValidator,
            IValidator<UpdateTrademarkModel> updateTrademarkValidator,
            IValidator<DeleteTrademarkModel> deleteTrademarkValidator
            ) : base(meadiator)
        {
            this.createTrademarkValidator = createTrademarkValidator;
            this.updateTrademarkValidator = updateTrademarkValidator;
            this.deleteTrademarkValidator = deleteTrademarkValidator;
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ResponseBaseModel<GetTrademarkReponseModel>> GetColors()
        {
            var result = await Mediator.Send(new GetTrademarkQuery());

            return new ResponseBaseModel<GetTrademarkReponseModel>
            {
                Data = result.Adapt<GetTrademarkReponseModel>()
            };
        }



        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<ResponseBaseModel<CreateTrademarkReponseModel>>> CreateManufacturers(
       [FromBody] CreateTrademarkModel request)
        {
            var validator = await createTrademarkValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<CreateTrademarkQuery>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<CreateTrademarkReponseModel>
            {
                Data = result.Adapt<CreateTrademarkReponseModel>()
            };

            return response;
        }



        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ActionResult<ResponseBaseModel<UpdateTrademarkReponseModel>>> UpdateManufacturers(
       [FromBody] UpdateTrademarkModel request)
        {
            var validator = await updateTrademarkValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<UpdateTrademarkQuery>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<UpdateTrademarkReponseModel>
            {
                Data = result.Adapt<UpdateTrademarkReponseModel>()
            };

            return response;
        }


        [AllowAnonymous]
        [HttpDelete("")]
        public async Task<ActionResult<ResponseBaseModel<DeleteTrademarkReponseModel>>> DeleteManufacturers(
     [FromBody] DeleteTrademarkModel request)
        {
            var validator = await deleteTrademarkValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<DeleteTrademarkQuery>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<DeleteTrademarkReponseModel>
            {
                Data = result.Adapt<DeleteTrademarkReponseModel>()
            };

            return response;
        }
    }
}
