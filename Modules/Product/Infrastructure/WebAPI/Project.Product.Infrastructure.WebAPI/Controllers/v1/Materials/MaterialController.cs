using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Put;
using Project.Product.Integration.Materials.Command;
using Project.Product.Integration.Materials.Query;


namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials
{
    public class MaterialController : CommonController
    {
        private readonly IValidator<CreateMaterialsModel> createMaterialsValidator;
        private readonly IValidator<UpdateMaterialsModel> updateMaterialsValidator;
        private readonly IValidator<DeleteMaterialsModel> deleteMaterialsValidator;
        public MaterialController(ISender meadiator,
            IValidator<CreateMaterialsModel> createMaterialsValidator,
            IValidator<UpdateMaterialsModel> updateMaterialsValidator,
            IValidator<DeleteMaterialsModel> deleteMaterialsValidator
            ) : base(meadiator)
        {
            this.createMaterialsValidator = createMaterialsValidator;
            this.updateMaterialsValidator = updateMaterialsValidator;
            this.deleteMaterialsValidator = deleteMaterialsValidator;
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ResponseBaseModel<GetMaterialsReponseModel>> GetColors()
        {
            var result = await Mediator.Send(new GetMaterialQuery());

            return new ResponseBaseModel<GetMaterialsReponseModel>
            {
                Data = result.Adapt<GetMaterialsReponseModel>()
            };
        }



        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<ResponseBaseModel<CreateMaterialsReponseModel>>> CreateManufacturers(
       [FromBody] CreateMaterialsModel request)
        {
            var validator = await createMaterialsValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<CreateMaterialCommand>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<CreateMaterialsReponseModel>
            {
                Data = result.Adapt<CreateMaterialsReponseModel>()
            };

            return response;
        }



        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ActionResult<ResponseBaseModel<UpdateMaterialsReponseModel>>> UpdateManufacturers(
       [FromBody] UpdateMaterialsModel request)
        {
            var validator = await updateMaterialsValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<UpdateMaterialCommand>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<UpdateMaterialsReponseModel>
            {
                Data = result.Adapt<UpdateMaterialsReponseModel>()
            };

            return response;
        }


        [AllowAnonymous]
        [HttpDelete("")]
        public async Task<ActionResult<ResponseBaseModel<DeleteMaterialsReponseModel>>> DeleteManufacturers(
     [FromBody] DeleteMaterialsModel request)
        {
            var validator = await deleteMaterialsValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<DeleteMaterialCommand>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<DeleteMaterialsReponseModel>
            {
                Data = result.Adapt<DeleteMaterialsReponseModel>()
            };

            return response;
        }
    }
}
