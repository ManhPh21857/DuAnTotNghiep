using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Enums;
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

        [Authorize(Roles = nameof(Role.MaterialView))]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<MaterialResponseModel>>> GetMaterial()
        {
            var result = await this.Mediator.Send(new GetMaterialQuery());

            return new ResponseBaseModel<MaterialResponseModel>
            {
                Data = result.Adapt<MaterialResponseModel>()
            };
        }

        [Authorize(Roles = nameof(Role.MaterialEdit))]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> UpdateMaterial(UpdateMaterialRequestModel request)
        {
            var validator = await this.materialValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateMaterialCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.MaterialDelete))]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandProductBase>> DeleteMaterial(DeleteMaterialRequestModel request)
        {

            var command = request.Adapt<DeleteMaterialCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [Authorize(Roles = nameof(Role.MaterialEdit))]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandProductBase>> ReactiveMaterial(DeleteMaterialRequestModel request)
        {

            var command = request.Adapt<ReactiveMaterialCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }
    }
}
