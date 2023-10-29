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
using Project.Product.Integration.Materials.Command;
using Project.Product.Integration.Materials.Query;


namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials
{
    public class MaterialController : CommonController
    {
        private readonly IValidator<UpdateMaterialRequestModel> materialValidator;
        public MaterialController(ISender mediator, IValidator<UpdateMaterialRequestModel> materialValidator) : base(mediator)
        {
            this.materialValidator = materialValidator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<MaterialResponseModel>>> GetColors()
        {
            var result = await this.Mediator.Send(new GetMaterialQuery());

            return new ResponseBaseModel<MaterialResponseModel>
            {
                Data = result.Adapt<MaterialResponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandBaseModel>>> UpdateColors(UpdateMaterialRequestModel request)
        {
            var validator = await this.materialValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateMaterialCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandBaseModel>> DeleteColors(DeleteMaterialRequestModel request)
        {

            var command = request.Adapt<DeleteMaterialCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandBaseModel>> ReactiveColors(DeleteMaterialRequestModel request)
        {

            var command = request.Adapt<ReactiveMaterialCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }
    }
}
