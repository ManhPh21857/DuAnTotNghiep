using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers.Put;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers1.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers1.Post;
using Project.Product.Integration.Manufacturers;


namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers1
{
    public class ManufacturerController : ProductController
    {
        private readonly IValidator<CreateManufacturerModel> createManufacturerValidator;
        private readonly IValidator<UpdateManufacturerModel> updateManufacturerValidator;
        private readonly IValidator<DeleteManufacturerModel> deleteManufacturerValidator;
        public ManufacturerController(ISender mediator, 
            IValidator<CreateManufacturerModel> createManufacturerValidator,
            IValidator<UpdateManufacturerModel> updateManufacturerValidator,
            IValidator<DeleteManufacturerModel> deleteManufacturerValidator
                                     ) :base(mediator)
        {
            this.updateManufacturerValidator = updateManufacturerValidator;
            this.createManufacturerValidator = createManufacturerValidator;
            this.deleteManufacturerValidator = deleteManufacturerValidator;
        }
        [AllowAnonymous]
        [HttpGet("manufacturers")]
        public async Task<ResponseBaseModel<ManufacturerReponseModel>> GetManufacturer()
        {
            var result = await Mediator.Send(new GetManufacturerQuery());
            return new ResponseBaseModel<ManufacturerReponseModel>
            {
                Data = result.Adapt<ManufacturerReponseModel>()
            };
        }
        [AllowAnonymous]
        [HttpPost("createmanufacturers")]
        public async Task<ActionResult<ResponseBaseModel<CreateManufacturerReponseModel>>> CreateManufacturers(
       [FromBody] CreateManufacturerModel request)
        {
            var validator = await createManufacturerValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<AddManufacturerQuery>();
          

            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<CreateManufacturerReponseModel>
            {
                Data = result.Adapt<CreateManufacturerReponseModel>()
            };

            return response;
        }


        [AllowAnonymous]
        [HttpPut("updatemanufacturers")]
        public async Task<ActionResult<ResponseBaseModel<UpdateManufacturerReponseModel>>> UpdateManufacturers(
       [FromBody] UpdateManufacturerModel request)
        {
            var validator = await updateManufacturerValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<UpdateManufacturerQuery>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<UpdateManufacturerReponseModel>
            {
                Data = result.Adapt<UpdateManufacturerReponseModel>()
            };

            return response;
        }


        [AllowAnonymous]
        [HttpDelete("deletemanufacturers")]
        public async Task<ActionResult<ResponseBaseModel<DeleteManufacturerReponseModel>>> DeleteManufacturers(
     [FromBody] DeleteManufacturerModel request)
        {
            var validator = await deleteManufacturerValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<DeleteManufacturerQuery>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<DeleteManufacturerReponseModel>
            {
                Data = result.Adapt<DeleteManufacturerReponseModel>()
            };

            return response;
        }
    }
}
